using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public RandomSpawner randomspawner;
    public PlayerMovement playerMovement;



    public bool CanDealDamage;
    public bool HasDealDamage;
    public EnemyHealth enemyHealth;
    public int damageAmount=1;

    
    
    private void Start()
    {
        CanDealDamage = false;
        randomspawner = GameObject.Find("EnemySpawner").GetComponent<RandomSpawner>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        
        
        
    }

    public void StartDealDamage()
    {
        CanDealDamage = true;
    }
    public void EndDealDamage()
    {
        CanDealDamage = false;
        HasDealDamage = false;
    }



    public void OnTriggerEnter(Collider other)
    {
        if (playerMovement.Action == PlayerMovement.ActionState.Attack && CanDealDamage == true && HasDealDamage == false)
        {
            if (other.tag == "Enemy")
            {
                
                
                other.GetComponent<EnemyHealth>().health = other.GetComponent<EnemyHealth>().health - damageAmount;
                HasDealDamage = true;


            }
            
            if (other.tag == "Animal")
            {
                Debug.Log(other.name + "Hit");
                Destroy(other.gameObject);
            }
        }

        
    }

    
}
