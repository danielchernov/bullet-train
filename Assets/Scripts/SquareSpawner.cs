using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject squareToSpawn;

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private SquareManager _squareManager;

    [SerializeField]
    private int minSquareAmountToSpawn = 2;

    [SerializeField]
    private int maxSquareAmountToSpawn = 5;

    [SerializeField]
    private float minWaitTime = 2f;

    [SerializeField]
    private float maxWaitTime = 4f;

    [SerializeField]
    private float spawnRange = 30f;

    void Start()
    {
        StartCoroutine(SpawningRoutine());
    }

    void SpawnSquares(int amountToSpawn)
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            Vector3 randomSpawnPos = new Vector3(
                _player.transform.position.x + Random.Range(-spawnRange, spawnRange),
                _player.transform.position.y + Random.Range(-spawnRange, spawnRange),
                _player.transform.position.z
            );

            Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

            GameObject spawnedSquare = Instantiate(
                squareToSpawn,
                randomSpawnPos,
                randomRotation,
                transform
            );

            spawnedSquare.transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().color =
                new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);

            float cratesMultiplier = 1 + (_squareManager.AmountOfSquares / 40f);
            spawnedSquare.transform.localScale *= Random.Range(
                0.5f * cratesMultiplier,
                2.5f * cratesMultiplier
            );
        }
    }

    IEnumerator SpawningRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            SpawnSquares(Random.Range(minSquareAmountToSpawn, maxSquareAmountToSpawn));
        }
    }
}
