using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    private SquareManager _squareManager;
    private Animator squareAnimator;
    private Coroutine wait;

    private AudioSource _sfxAudio;

    [SerializeField]
    private AudioClip[] _grabClip;

    void Start()
    {
        _squareManager = GameObject.Find("SquareContainer").GetComponent<SquareManager>();
        squareAnimator = GetComponentInChildren<Animator>();

        _sfxAudio = GameObject
            .Find("AudioManager")
            .transform.GetChild(1)
            .GetComponent<AudioSource>();

        wait = StartCoroutine(WaitAndVanish());
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            StartCoroutine(ShrinkAndAdd(collider.gameObject));
        }
        if (collider.tag == "Platform" && transform.parent != _squareManager.transform)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator ShrinkAndAdd(GameObject player)
    {
        Animator playerAnimator = player.GetComponentInChildren<Animator>();
        squareAnimator.SetTrigger("isShrinking");
        playerAnimator.SetTrigger("isEating");

        _sfxAudio.PlayOneShot(_grabClip[Random.Range(0, _grabClip.Length)], 1f);

        yield return new WaitForSeconds(0.5f);

        squareAnimator.ResetTrigger("isShrinking");
        playerAnimator.ResetTrigger("isEating");

        StopCoroutine(wait);
        StartCoroutine(_squareManager.AddSquare(gameObject));
        //this.enabled = false;
    }

    IEnumerator WaitAndVanish()
    {
        yield return new WaitForSeconds(Random.Range(30f, 50f));
        squareAnimator.SetTrigger("isShrinking");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
