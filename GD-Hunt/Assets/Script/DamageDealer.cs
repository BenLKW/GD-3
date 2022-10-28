using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public RandomSpawner randomspawner;
    public PlayerMovement playerMovement;

    public Quest quest;
    public bool isQuesting;

    void Start()
    {
        randomspawner = GameObject.Find("EnemySpawner").GetComponent<RandomSpawner>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        isQuesting = false;
    }

    private void Update()
    {
        StartQuesting();
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

                if (isQuesting == true)
                {
                    quest.currentAmount++;
                }

            }
            
            if (other.tag == "Animal")
            {
                Debug.Log(other.name + "Hit");
                Destroy(other.gameObject);
            }
        }
        
    }
    void StartQuesting()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            quest = GameObject.Find("Quest").GetComponent<Quest>();
            isQuesting = true;
        }
    }
    
}
