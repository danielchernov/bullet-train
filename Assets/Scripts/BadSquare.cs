using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadSquare : MonoBehaviour
{
    GameManager _gameManager;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(_gameManager.KillPlayer());
        }

        if (collision.gameObject.tag == "Platform")
        {
            Destroy(gameObject);
        }
    }
}
