using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public RandomSpawner randomspawner;
    public PlayerMovement playerMovement;

    public bool enemyKilled;
    

    void Start()
    {
        randomspawner = GameObject.Find("EnemySpawner").GetComponent<RandomSpawner>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        enemyKilled = false;
    }

    private void Update()
    {
        
    }

    

    void OnTriggerEnter(Collider other)
    {
        if (playerMovement.Action == PlayerMovement.ActionState.Attack)
        {
            if (other.tag == "Enemy")
            {
                Debug.Log(other.name + "Hit");
                Destroy(other.gameObject);
                randomspawner.enemyCount -= 1;
                enemyKilled = true;


            }
            if (other.tag == "Animal")
            {
                Debug.Log(other.name + "Hit");
                Destroy(other.gameObject);
            }
        }
        
    }

    
}
