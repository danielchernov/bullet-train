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

    //private Animator playerAnimator;

    private bool isDashing = false;

    public float startDashTime = 0;
    public float dashCooldown = 0;
    public float dashSpeed = 0;

    private Vector3 dashMovement;

    [SerializeField]
    private AudioSource _sfxAudio;

    [SerializeField]
    private AudioClip _dashClip;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        dashTime = startDashTime;
        dashCooldownTime = dashCooldown;

        //playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isDashing)
        {
            if (dashCooldownTime <= 0)
            {
                if (Input.GetButton("Jump"))
                {
                    dashStartPosition = transform.position;
                    isDashing = true;
                    //playerAnimator.SetBool("isDashing", true);

                    dashCooldownTime = dashCooldown;

                    _sfxAudio.PlayOneShot(_dashClip, 1f);
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

                //playerAnimator.SetBool("isDashing", false);

                dashTime = startDashTime;
            }
            else
            {
                dashTime -= Time.deltaTime;

                dashMovement = transform.right;

                playerRb.AddForce(dashMovement * dashSpeed * Time.deltaTime, ForceMode2D.Impulse);
            }
        }
    }
}
