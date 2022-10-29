using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject SpawnObject;
    public Transform spawnpoint;
    public int xPos;
    public int zPos;
    public int enemyCount;
    public float spawnDelay;
    public float spawnTime;
    


    void Start()
    {
        //InvokeRepeating("EnemySpawn", spawnTime, spawnDelay);
        //StartCoroutine(SpawnMobs());

    }

    void Update()
    {
        enemyNumDetector();

    }


    void enemyNumDetector()
    {
        if (enemyCount == 0)
        {
            StartCoroutine(SpawnMobs());
        }
    }
    
    IEnumerator SpawnMobs()
    {
        while (enemyCount < 10)
        {
            Instantiate(SpawnObject, spawnpoint.position, Quaternion.identity);
            enemyCount += 1;
            yield return new WaitForSecondsRealtime(1f);

        }
    }
}
