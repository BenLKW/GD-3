using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public RandomSpawner randomspawner;
    public PlayerMovement playerMovement;



    public bool canDealDamage;
    public bool hasDealDamage;
    [SerializeField] float weaponLength;

    public EnemyHealth enemyHealth;
    public int damageAmount=1;

    
    
    private void Start()
    {
        canDealDamage = false;
        randomspawner = GameObject.Find("EnemySpawner").GetComponent<RandomSpawner>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        
        
        
    }

    
    public void StartDealDamage()
    {
        canDealDamage = true;
    }
    public void EndDealDamage()
    {
        canDealDamage = false;
        hasDealDamage = false;
    }



    public void OnTriggerEnter(Collider other)
    {
        if (canDealDamage == true && hasDealDamage == false)
        {
            if (other.tag == "Enemy")
            {
                
                
                other.GetComponent<EnemyHealth>().health = other.GetComponent<EnemyHealth>().health - damageAmount;
                other.GetComponent<Animator>().SetTrigger("GetHit");
                hasDealDamage = true;


            }
            
            if (other.tag == "Animal")
            {
                Debug.Log(other.name + "Hit");
                Destroy(other.gameObject);
            }
        }

        
    }

    
}
