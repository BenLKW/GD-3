using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject animal;
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
            xPos = Random.Range(-20,20);
            zPos = Random.Range(-20, 25);
            Instantiate(animal, new Vector3(xPos, 0, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            enemyCount += 1;

        }
    }

}
