using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPlatform : MonoBehaviour
{
    private bool _playerOnTop = false;
    private SquareManager _squareManager;
    private Coroutine SquareReceiving;

    void Start()
    {
        _squareManager = GameObject.Find("SquareContainer").GetComponent<SquareManager>();
    }

    void Update() { }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            _playerOnTop = true;
            SquareReceiving = StartCoroutine(ReceiveSquares());
        }

        if (collider.tag == "Platform")
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            StopCoroutine(SquareReceiving);
            _playerOnTop = false;
        }
    }

    IEnumerator ReceiveSquares()
    {
        while (_playerOnTop)
        {
            StartCoroutine(_squareManager.RemoveSquare());

            yield return new WaitForSeconds(0.25f);
        }
    }
}
