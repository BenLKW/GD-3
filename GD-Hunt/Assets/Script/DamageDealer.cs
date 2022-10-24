using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public RandomSpawner randomspawner;
    
    

    void Start()
    {
        randomspawner = GameObject.Find("EnemySpawner").GetComponent<RandomSpawner>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
         Debug.Log(other.name + "Hit");
         Destroy(other.gameObject);
            randomspawner.enemyCount -= 1;
        
            
        }
        if (other.tag == "Animal")
        {
            Debug.Log(other.name + "Hit");
            Destroy(other.gameObject);
        }
    }
}
