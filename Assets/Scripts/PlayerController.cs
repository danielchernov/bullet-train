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

    public bool isInputLocked = false;

    [SerializeField]
    private SquareManager _squareManager;

    private int currentSquareAmount = 0;
    private int lastSquareAmount = 0;

    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isInputLocked)
        {
            moveInput = Input.GetAxis("Vertical");

            turnInput = Input.GetAxis("Horizontal");
        }

        currentSquareAmount = _squareManager.AmountOfSquares;

        if (currentSquareAmount > lastSquareAmount)
        {
            lastSquareAmount = currentSquareAmount;

            if (_squareManager.AmountOfSquares < 20)
            {
                moveSpeed *= 1.01f;
                turnSpeed *= 1.01f;
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
