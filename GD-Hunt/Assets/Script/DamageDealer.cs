using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public RandomSpawner randomspawner;
    public PlayerMovement playerMovement;
    
    

    void Start()
    {
        randomspawner = GameObject.Find("EnemySpawner").GetComponent<RandomSpawner>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (playerMovement.Action == PlayerMovement.ActionState.Attack)
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
}
