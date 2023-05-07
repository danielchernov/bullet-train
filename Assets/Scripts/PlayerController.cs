using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float intialMoveSpeed = 3200;
    public float initialTurnSpeed = 200;

    [SerializeField]
    private float moveSpeed = 3200;

    [SerializeField]
    private float turnSpeed = 200;

    float moveInput;
    float turnInput;

    Rigidbody2D _playerBody;
    Animator _playerAnimator;

    //public bool isInputLocked = false;

    [SerializeField]
    private SquareManager _squareManager;

    private float currentSquareAmount = 0;
    private float lastSquareAmount = 0;

    [SerializeField]
    private AudioSource _walkingAudio;

    [SerializeField]
    private AudioClip[] _walkingSFX;

    [SerializeField]
    private GameObject _pauseMenu;

    void Start()
    {
        _playerBody = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        //if (!isInputLocked)
        //{
        moveInput = Input.GetAxis("Vertical");

        turnInput = Input.GetAxis("Horizontal");
        //}
        if (moveInput > 0 && !_playerAnimator.GetBool("isWalking"))
        {
            _playerAnimator.SetBool("isWalking", true);
        }
        if (moveInput == 0 && _playerAnimator.GetBool("isWalking"))
        {
            _playerAnimator.SetBool("isWalking", false);
        }

        if (moveInput > 0 && !_walkingAudio.isPlaying && !_pauseMenu.activeSelf)
        {
            _walkingAudio.PlayOneShot(_walkingSFX[Random.Range(0, _walkingSFX.Length)], 0.4f);
        }
        else if (moveInput == 0 && _walkingAudio.isPlaying)
        {
            _walkingAudio.Stop();
        }

        currentSquareAmount = _squareManager.AmountOfSquares;

        if (currentSquareAmount != lastSquareAmount && moveInput > 0)
        {
            lastSquareAmount = currentSquareAmount;

            if (_squareManager.AmountOfSquares < 10)
            {
                moveSpeed = intialMoveSpeed * Mathf.Pow(1.07f, _squareManager.AmountOfSquares);
                ;
                turnSpeed = initialTurnSpeed * Mathf.Pow(1.07f, _squareManager.AmountOfSquares);
            }
            else if (_squareManager.AmountOfSquares < 20)
            {
                moveSpeed = intialMoveSpeed * Mathf.Pow(1.05f, _squareManager.AmountOfSquares);
                turnSpeed = initialTurnSpeed * Mathf.Pow(1.05f, _squareManager.AmountOfSquares);
            }
            else if (_squareManager.AmountOfSquares < 40)
            {
                moveSpeed = intialMoveSpeed * Mathf.Pow(1.04f, _squareManager.AmountOfSquares);
                turnSpeed = initialTurnSpeed * Mathf.Pow(1.04f, _squareManager.AmountOfSquares);
            }
            else
            {
                moveSpeed = intialMoveSpeed * Mathf.Pow(1.03f, _squareManager.AmountOfSquares);
                turnSpeed = initialTurnSpeed * Mathf.Pow(1.03f, _squareManager.AmountOfSquares);
            }
        }
    }

    void FixedUpdate()
    {
        if (moveInput > 0)
        {
            _playerBody.AddForce(
                transform.right * moveInput * moveSpeed * Time.deltaTime,
                ForceMode2D.Impulse
            );
        }

        if (turnInput != 0)
        {
            _playerBody.AddTorque(-turnInput * turnSpeed * Time.deltaTime, ForceMode2D.Impulse);
        }
    }
}
