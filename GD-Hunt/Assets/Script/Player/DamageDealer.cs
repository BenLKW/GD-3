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

    public EnemyHealth enemyHealth;
    public int damageAmount=1;
    
    private void Start()
    {
        randomspawner = GameObject.Find("EnemySpawner").GetComponent<RandomSpawner>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        TargetLock = GameObject.Find("Main Camera").GetComponent<TargetLock>();
        quest = GameObject.Find("Quest").GetComponent<Quest>();
        enemyHealth = GameObject.Find("pig").GetComponent<EnemyHealth>();
        isQuesting = false;
        
    }

    public void Update()
    {
        StartQuesting();
    }



    public void OnTriggerEnter(Collider collision)
    {
        if (playerMovement.Action == PlayerMovement.ActionState.Attack)
        {
            if (collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemyComponent))
            {

                enemyComponent.TakeDamage(1);
                TargetLock.isTargeting = false;
                randomspawner.enemyCount -= 1;

                if (isQuesting == true)
                {
                    quest.currentAmount++;
                }


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
