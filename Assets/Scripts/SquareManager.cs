using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareManager : MonoBehaviour
{
    public int AmountOfSquares = 0;

    [SerializeField]
    private PlayerController _player;
    private Rigidbody2D _connectedRb;

    public IEnumerator AddSquare(GameObject square)
    {
        AmountOfSquares++;

        Animator squareAnimator = square.GetComponentInChildren<Animator>();
        Quaternion squareRotation = square.transform.rotation;
        _player.transform.localScale *= 1.005f;

        if (AmountOfSquares == 1)
        {
            Vector3 spawnPos = new Vector3(-1.4f, 0, 0);

            square.transform.parent = _player.transform;
            square.transform.rotation = _player.transform.rotation;
            square.transform.localPosition = spawnPos;
            square.transform.parent = transform;

            _connectedRb = _player.GetComponent<Rigidbody2D>();
        }
        else
        {
            Vector3 spawnPos = new Vector3(-1.2f, 0, 0);
            Transform lastSquare = transform.GetChild(transform.childCount - 1);

            square.transform.parent = lastSquare.transform;
            square.transform.rotation = lastSquare.transform.rotation;
            square.transform.localPosition = spawnPos;
            square.transform.parent = transform;

            _connectedRb = lastSquare.GetComponent<Rigidbody2D>();
        }

        HingeJoint2D hingeJoint = square.AddComponent<HingeJoint2D>();

        hingeJoint.autoConfigureConnectedAnchor = false;
        hingeJoint.anchor = new Vector2(0.5f, 0);
        hingeJoint.connectedAnchor = new Vector2(-0.6f, 0);
        hingeJoint.connectedBody = _connectedRb;

        JointAngleLimits2D limits = hingeJoint.limits;
        limits.min =
            ((squareRotation.eulerAngles.z) - 30) - square.transform.rotation.eulerAngles.z;
        limits.max =
            ((squareRotation.eulerAngles.z) + 30) - square.transform.rotation.eulerAngles.z;
        hingeJoint.limits = limits;

        hingeJoint.enabled = true;

        square.GetComponent<BoxCollider2D>().isTrigger = false;

        yield return new WaitForSeconds(0.2f);

        //squareAnimator.ResetTrigger("isShrinking");
        squareAnimator.SetTrigger("isGrowing");
    }
}
