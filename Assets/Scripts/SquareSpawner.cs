using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject squareToSpawn;

    [SerializeField]
    private int amountToSpawn = 200;

    void Start()
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            Vector3 randomSpawnPos = new Vector3(
                Random.Range(-30f, 30f),
                Random.Range(-30f, 30f),
                0
            );

            Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

            GameObject spawnedSquare = Instantiate(squareToSpawn, randomSpawnPos, randomRotation);

            spawnedSquare.transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().color =
                new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);

            //spawnedSquare.transform.localScale *= Random.Range(0.5f, 2f);
        }
    }
}
