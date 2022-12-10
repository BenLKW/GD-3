using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttackDetector : MonoBehaviour
{
    public EnemyAI enemyAI;
    
    public Health playerHealth;
    
    public bool canDealDamage;
    public bool hasDealDamage;
    [SerializeField] float weaponLength;
    private void Start()
    {
        canDealDamage = false;

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
            if (other.tag == "Player")
            {



                Dealdamage();
                hasDealDamage = true;


            }

        }
            
       
        
    }
    void Dealdamage()
    {
        playerHealth = GameObject.Find("Player").GetComponent<Health>();
        playerHealth.TakeDamage(1);
       
    }


    

}
