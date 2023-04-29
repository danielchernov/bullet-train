using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSquare : MonoBehaviour
{
    private SquareManager _squareManager;

    void Start()
    {
        _squareManager = GameObject.Find("SquareContainer").GetComponent<SquareManager>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            StartCoroutine(ShrinkAndAdd(collider.gameObject));
        }
    }

    IEnumerator ShrinkAndAdd(GameObject player)
    {
        Animator squareAnimator = GetComponentInChildren<Animator>();
        Animator playerAnimator = player.GetComponentInChildren<Animator>();
        squareAnimator.SetTrigger("isShrinking");
        playerAnimator.SetTrigger("isEating");
        yield return new WaitForSeconds(0.5f);
        squareAnimator.ResetTrigger("isShrinking");
        playerAnimator.ResetTrigger("isEating");
        //Debug.Log("here!");
        StartCoroutine(_squareManager.AddSquare(gameObject));
        this.enabled = false;
    }
}
