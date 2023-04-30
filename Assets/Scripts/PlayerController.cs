using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 100;
    public float turnSpeed = 100;

    float moveInput;
    float turnInput;

    Rigidbody2D playerBody;

    //public bool isInputLocked = false;

    [SerializeField]
    private SquareManager _squareManager;

    private float currentSquareAmount = 0;
    private float lastSquareAmount = 0;

    [SerializeField]
    private AudioSource _walkingAudio;

    [SerializeField]
    private AudioClip[] _walkingSFX;

    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //if (!isInputLocked)
        //{
        moveInput = Input.GetAxis("Vertical");

        turnInput = Input.GetAxis("Horizontal");
        //}

        if (moveInput > 0 && !_walkingAudio.isPlaying)
        {
            _walkingAudio.PlayOneShot(_walkingSFX[Random.Range(0, _walkingSFX.Length)], 0.5f);
        }
        else if (moveInput == 0 && _walkingAudio.isPlaying)
        {
            _walkingAudio.Stop();
        }

        currentSquareAmount = _squareManager.AmountOfSquares;

        if (currentSquareAmount > lastSquareAmount)
        {
            lastSquareAmount = currentSquareAmount;

            if (_squareManager.AmountOfSquares < 20)
            {
                moveSpeed *= 1.02f;
                turnSpeed *= 1.02f;
            }
            else
            {
                moveSpeed *= 1.01f;
                turnSpeed *= 1.01f;
            }
        }
    }

    void FixedUpdate()
    {
        if (moveInput > 0)
        {
            playerBody.AddForce(
                transform.right * moveInput * moveSpeed * Time.deltaTime,
                ForceMode2D.Impulse
            );
        }

        if (turnInput != 0)
        {
            //transform.Rotate(Vector3.forward, -turnInput * turnSpeed * Time.deltaTime);
            playerBody.AddTorque(-turnInput * turnSpeed * Time.deltaTime, ForceMode2D.Impulse);
        }
    }
}
