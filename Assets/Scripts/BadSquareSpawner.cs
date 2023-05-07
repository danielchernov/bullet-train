using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadSquareSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private SquareManager _squareManager;

    [SerializeField]
    private GameObject badSquareToSpawn;

    [SerializeField]
    private int minBadAmountToSpawn = 2;

    [SerializeField]
    private int maxBadAmountToSpawn = 5;

    [SerializeField]
    private float spawnBadRange = 50f;

    [SerializeField]
    private float minWaitTime = 2f;

    [SerializeField]
    private float maxWaitTime = 4f;

    float randomX;
    float randomY;
    Vector3 randomSpawnPos;

    void Start()
    {
        StartCoroutine(SpawningRoutine());
    }

    void SpawnBadSquares(int amountToSpawn)
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            switch (Random.Range(0, 4))
            {
                case 0:
                    randomX = Random.Range(20, spawnBadRange);
                    randomY = Random.Range(20, spawnBadRange);
                    randomSpawnPos = new Vector3(
                        _player.transform.position.x + randomX,
                        _player.transform.position.y + randomY,
                        _player.transform.position.z
                    );
                    break;
                case 1:
                    randomX = Random.Range(20, spawnBadRange);
                    randomY = Random.Range(-spawnBadRange, -20);
                    randomSpawnPos = new Vector3(
                        _player.transform.position.x + randomX,
                        _player.transform.position.y + randomY,
                        _player.transform.position.z
                    );
                    break;
                case 2:
                    randomX = Random.Range(-spawnBadRange, -20);
                    randomY = Random.Range(20, spawnBadRange);
                    randomSpawnPos = new Vector3(
                        _player.transform.position.x + randomX,
                        _player.transform.position.y + randomY,
                        _player.transform.position.z
                    );
                    break;
                case 3:
                    randomX = Random.Range(-spawnBadRange, -20);
                    randomY = Random.Range(-spawnBadRange, -20);
                    randomSpawnPos = new Vector3(
                        _player.transform.position.x + randomX,
                        _player.transform.position.y + randomY,
                        _player.transform.position.z
                    );
                    break;
            }

            GameObject spawnedSquare = Instantiate(
                badSquareToSpawn,
                randomSpawnPos,
                Quaternion.identity,
                transform
            );
        }
    }

    IEnumerator SpawningRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            SpawnBadSquares(Random.Range(minBadAmountToSpawn, maxBadAmountToSpawn));
        }
    }
}
