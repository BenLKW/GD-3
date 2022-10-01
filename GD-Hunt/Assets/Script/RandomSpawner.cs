using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject animal;

    public float timeToSpawn;
    private float currentTimeToSpawn;



    void Update()
    {
        if (currentTimeToSpawn > 0)
        {
            currentTimeToSpawn -= Time.deltaTime;
        }
        else
        {
            Spawn();
            currentTimeToSpawn = timeToSpawn;
        }
    }

    public void Spawn()
    {
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-10, 11), 1, Random.Range(-10, 11));
        Instantiate(animal, randomSpawnPosition, Quaternion.identity);
    }
}
