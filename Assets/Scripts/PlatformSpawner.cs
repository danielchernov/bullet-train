using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField]
    private int _platformsToSpawn = 10;

    [SerializeField]
    private float _distanceFromCenter = 20;

    [SerializeField]
    private GameObject _platform;

    private Vector3 randomPos;

    void Start()
    {
        for (int i = 0; i < _platformsToSpawn; i++)
        {
            switch (Random.Range(1, 5))
            {
                case 1:
                    randomPos = new Vector3(
                        Random.Range(-250f, -_distanceFromCenter),
                        Random.Range(-250f, 250f),
                        0
                    );
                    break;
                case 2:
                    randomPos = new Vector3(
                        Random.Range(_distanceFromCenter, 250f),
                        Random.Range(-250f, 250f),
                        0
                    );
                    break;
                case 3:
                    randomPos = new Vector3(
                        Random.Range(-250f, 250f),
                        Random.Range(-250f, -_distanceFromCenter),
                        0
                    );
                    break;
                case 4:
                    randomPos = new Vector3(
                        Random.Range(-250f, 250f),
                        Random.Range(_distanceFromCenter, 250f),
                        0
                    );
                    break;
                default:
                    break;
            }

            GameObject platform = Instantiate(
                _platform,
                randomPos,
                Quaternion.Euler(0, 0, 45),
                transform
            );

            SpriteRenderer platformSprite = platform.GetComponent<SpriteRenderer>();
            platformSprite.color = new Color(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                1
            );

            int randomSpawn = Random.Range(10, 20);
            platform.transform.localScale = new Vector3(randomSpawn, randomSpawn, 1);
        }
    }
}
