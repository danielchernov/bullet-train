using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DashController : MonoBehaviour
{
    public Vector3 dashStartPosition;

    private float dashTime;
    private float dashCooldownTime;

    private Collider2D playerCollider;
    private Rigidbody2D playerRb;

    private Animator _playerAnimator;

    private bool isDashing = false;

    public float startDashTime = 0;
    public float dashCooldown = 0;
    public float dashSpeed = 0;

    [SerializeField]
    float currentStartDashTime;

    [SerializeField]
    float currentDashCooldown;

    [SerializeField]
    float currentDashSpeed;

    private Vector3 dashMovement;

    [SerializeField]
    private AudioSource _sfxAudio;

    [SerializeField]
    private AudioClip _dashClip;

    [SerializeField]
    private SquareManager _squareManager;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        dashTime = startDashTime;
        dashCooldownTime = dashCooldown;

        _playerAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!isDashing)
        {
            if (dashCooldownTime <= 0)
            {
                if (Input.GetButton("Jump"))
                {
                    currentStartDashTime =
                        startDashTime * (1 + (_squareManager.AmountOfSquares / 40));
                    currentDashCooldown =
                        dashCooldown / (1 + (_squareManager.AmountOfSquares / 40));
                    currentDashSpeed = dashSpeed * (1 + (_squareManager.AmountOfSquares / 40));

                    dashStartPosition = transform.position;
                    isDashing = true;
                    _playerAnimator.SetTrigger("isDashing");

                    dashCooldownTime = currentDashCooldown;

                    _sfxAudio.PlayOneShot(_dashClip, 1.2f);
                }
            }
            else
            {
                dashCooldownTime -= Time.deltaTime;
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                isDashing = false;

                _playerAnimator.ResetTrigger("isDashing");

                dashTime = currentStartDashTime;
            }
            else
            {
                dashTime -= Time.deltaTime;

                dashMovement = transform.right;

                playerRb.AddForce(
                    dashMovement * currentDashSpeed * Time.deltaTime,
                    ForceMode2D.Impulse
                );
            }
        }
    }
}
