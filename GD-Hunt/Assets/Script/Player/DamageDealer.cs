using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    
    public bool canDealDamage;
    public bool hasDealDamage;
    [SerializeField] float weaponLength;

    
    public int damageAmount=1;

    
    
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
            if (other.tag == "Enemy")
            {
                
                
                other.GetComponent<EnemyHealth>().health = other.GetComponent<EnemyHealth>().health - damageAmount;
                other.GetComponent<Animator>().SetTrigger("GetHit");
                hasDealDamage = true;


            }
            
            if (other.tag == "Animal")
            {
                other.GetComponent<FriendlyHealth>().health = other.GetComponent<FriendlyHealth>().health - damageAmount;
                hasDealDamage = true;
            }
        }

        
    }

    
}
