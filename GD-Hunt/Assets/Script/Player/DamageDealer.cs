using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public RandomSpawner randomspawner;
    public PlayerMovement playerMovement;
    
    
    

    public EnemyHealth enemyHealth;
    public int damageAmount=1;

    
    
    private void Start()
    {
        randomspawner = GameObject.Find("EnemySpawner").GetComponent<RandomSpawner>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        
        
        
    }

    



    public void OnTriggerEnter(Collider other)
    {
        if (playerMovement.Action == PlayerMovement.ActionState.Attack)
        {
            if (other.tag == "Enemy")
            {
                
                
                other.GetComponent<EnemyHealth>().health = other.GetComponent<EnemyHealth>().health - damageAmount;
                


            }
            
            if (other.tag == "Animal")
            {
                Debug.Log(other.name + "Hit");
                Destroy(other.gameObject);
            }
        }

        
    }

    
}
