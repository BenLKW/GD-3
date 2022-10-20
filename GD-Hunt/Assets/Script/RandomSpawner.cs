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
    

    
   

    void Start()
    {
        StartCoroutine (EnemySpawn());
        
    }

    IEnumerator EnemySpawn()
    {
        while (enemyCount<10)
        {
            Instantiate(SpawnObject, spawnpoint.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            enemyCount += 1;

        }
    }

}
