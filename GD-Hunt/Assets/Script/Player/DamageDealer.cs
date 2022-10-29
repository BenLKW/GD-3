using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public RandomSpawner randomspawner;
    public PlayerMovement playerMovement;
    public TargetLock TargetLock;

    public Quest quest;
    public bool isQuesting;


    void Start()
    {
        randomspawner = GameObject.Find("EnemySpawner").GetComponent<RandomSpawner>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        TargetLock = GameObject.Find("Main Camera").GetComponent<TargetLock>();
        quest = GameObject.Find("Quest").GetComponent<Quest>();
        isQuesting = false;
    }

    public void Update()
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
                TargetLock.isTargeting = false;
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
        if (isQuesting == true)
        {
            quest = GameObject.Find("Quest").GetComponent<Quest>();

        }
    }
}
