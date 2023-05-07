using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPlatform : MonoBehaviour
{
    private bool _playerOnTop = false;
    private SquareManager _squareManager;
    private Coroutine SquareReceiving;

    [SerializeField]
    private SpriteRenderer _platformRenderer;

    void Start()
    {
        _squareManager = GameObject.Find("SquareManager").GetComponent<SquareManager>();
        //_platformRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            _playerOnTop = true;
            SquareReceiving = StartCoroutine(ReceiveSquares());

            _platformRenderer.color *= 2f;
        }

        if (collider.tag == "BadSquare")
        {
            Destroy(collider.gameObject);
        }
        if (collider.tag == "Platform")
        {
            Destroy(collider.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            _playerOnTop = false;
            StopCoroutine(SquareReceiving);

            _platformRenderer.color /= 2f;
        }
    }

    IEnumerator ReceiveSquares()
    {
        while (_playerOnTop)
        {
            if (_squareManager.AmountOfSquares > 0)
            {
                StartCoroutine(_squareManager.RemoveSquare());

                transform.localScale *= 1.01f;
            }

            yield return new WaitForSeconds(0.25f);
        }
    }
}
