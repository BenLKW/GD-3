using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject SpawnObject;
    public Transform spawnpoint;
    public int xPos;
    public int zPos;
    [SerializeField]private int spawnAmount;
    public int enemyCount;
    public float spawnDelay;
    public float spawnTime;

    public Transform mobsInWorld;
    

    void Start()
    {
        

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
        while (enemyCount < spawnAmount)
        {
            Instantiate(SpawnObject, new Vector3(spawnpoint.position.x+Random.Range(-25,25),spawnpoint.position.y,spawnpoint.position.z+ Random.Range(-25, 25)), Quaternion.identity,mobsInWorld);
            enemyCount += 1;
            
            yield return new WaitForSecondsRealtime(1f);
            
        }
    }
}
