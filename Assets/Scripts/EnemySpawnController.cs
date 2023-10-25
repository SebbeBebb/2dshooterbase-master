using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] 
    GameObject EnemyPrefab;

    float spawnTimer = 0;

    [SerializeField] 
    float timeBetweenEnemies = 1.5f;

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > timeBetweenEnemies)
        {
            Instantiate(EnemyPrefab);
            spawnTimer = 0;
        }
    }
}
