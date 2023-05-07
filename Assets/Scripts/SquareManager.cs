using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SquareManager : MonoBehaviour
{
    public float AmountOfSquares = 0;
    public float ScoreTotal = 0;
    public float BestScore = 0;

    [SerializeField]
    private PlayerController _player;

    [SerializeField]
    private UI_Manager _pauseMenu;

    private Rigidbody2D _connectedRb;
    private Transform lastSquare;
    private Vector3 spawnPos;
    private HingeJoint2D squareHingeJoint;

    [SerializeField]
    private AudioSource _sfxAudio;

    [SerializeField]
    private AudioClip[] _receiveClip;

    [SerializeField]
    private ZoomCamera _zoomCamera;

    [SerializeField]
    private GameObject _scoreTextVFX;

    void Awake()
    {
        //PlayerPrefs.SetFloat("BestScore", 0);
        BestScore = PlayerPrefs.GetFloat("BestScore", 0);
    }

    void Start()
    {
        _pauseMenu.WriteBestScore(BestScore);
    }

    public IEnumerator AddSquare(GameObject square)
    {
        AmountOfSquares++;

        _pauseMenu.CrateAmount(AmountOfSquares);
        _zoomCamera.CamZoom();

        Animator squareAnimator = square.GetComponentInChildren<Animator>();
        Quaternion oldSquareRotation = square.transform.rotation;
        _player.transform.localScale *= 1.01f;

        if (square.GetComponent<HingeJoint2D>() == null)
        {
            squareHingeJoint = square.AddComponent<HingeJoint2D>();
        }

        if (AmountOfSquares == 1)
        {
            lastSquare = _player.transform;
            spawnPos = new Vector3(-1.4f * lastSquare.localScale.x, 0, 0);
        }
        else if (AmountOfSquares > 1)
        {
            lastSquare = transform.GetChild(transform.childCount - 1);
            spawnPos = new Vector3(-1.2f * lastSquare.localScale.x, 0, 0);
        }

        square.transform.parent = lastSquare.transform;
        square.transform.rotation = lastSquare.transform.rotation;
        square.transform.localPosition = spawnPos;
        square.transform.parent = transform;

        _connectedRb = lastSquare.GetComponent<Rigidbody2D>();

        squareHingeJoint.autoConfigureConnectedAnchor = false;

        if (_connectedRb != square.GetComponent<Rigidbody2D>())
        {
            squareHingeJoint.connectedBody = _connectedRb;
        }

        if (AmountOfSquares == 1)
        {
            squareHingeJoint.anchor = new Vector2(0.65f, 0);
            squareHingeJoint.connectedAnchor = new Vector2(-0.75f, 0);
        }
        else if (AmountOfSquares > 1)
        {
            squareHingeJoint.anchor = new Vector2(0.6f, 0);
            squareHingeJoint.connectedAnchor = new Vector2(-0.6f, 0);
        }

        JointAngleLimits2D limits = squareHingeJoint.limits;

        float minLimit =
            (Wrap(oldSquareRotation.eulerAngles.z, 180, -180) - 40)
            - Wrap(square.transform.rotation.eulerAngles.z, 180, -180);
        float maxLimit =
            (Wrap(oldSquareRotation.eulerAngles.z, 180, -180) + 40)
            - Wrap(square.transform.rotation.eulerAngles.z, 180, -180);

        limits.min = minLimit;
        limits.max = maxLimit;
        squareHingeJoint.limits = limits;

        square.GetComponent<BoxCollider2D>().isTrigger = false;

        yield return new WaitForSeconds(0.5f);

        squareAnimator.SetTrigger("isGrowing");
    }

    private float Wrap(float value, float max, float min)
    {
        max -= min;
        if (max == 0)
            return min;

        return value - max * (float)Mathf.Floor((value - min) / max);
    }

    public IEnumerator RemoveSquare()
    {
        if (transform.childCount > 0 && AmountOfSquares > 0)
        {
            GameObject score = Instantiate(
                _scoreTextVFX,
                _player.transform.position,
                Quaternion.identity
            );

            Destroy(score, 3f);

            score.GetComponentInChildren<TextMeshProUGUI>().text = "+ " + AmountOfSquares;

            AmountOfSquares--;
            ScoreTotal += transform.childCount;

            _player.transform.localScale /= 1.01f;

            _pauseMenu.CrateAmount(AmountOfSquares);
            _pauseMenu.WriteScore(ScoreTotal);
            _zoomCamera.CamZoom();

            GameObject square = transform.GetChild(transform.childCount - 1).gameObject;

            Animator squareAnimator = square.GetComponentInChildren<Animator>();

            squareAnimator.SetTrigger("isShrinking");

            _sfxAudio.PlayOneShot(_receiveClip[Random.Range(0, _receiveClip.Length)], 0.5f);

            yield return new WaitForSeconds(0.1f);

            Destroy(square);
        }
    }
}
