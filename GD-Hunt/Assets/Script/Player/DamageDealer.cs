using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{

    Collider AxeDetactor;
    public bool hasDealDamage;
    [SerializeField] float weaponLength;

    
    public int damageAmount=1;

    
    
    private void Start()
    {
        AxeDetactor = GetComponent<Collider>();
        AxeDetactor.enabled = false;

    }

    
    public void StartDealDamage()
    {
        AxeDetactor.enabled = true;
    }
    public void EndDealDamage()
    {
        AxeDetactor.enabled = false;
        hasDealDamage = false;
    }



    public void OnTriggerEnter(Collider other)
    {
        if (hasDealDamage == false)
        {
            if (other.tag == "Enemy")
            {
                
                
                other.GetComponent<EnemyHealth>().health = other.GetComponent<EnemyHealth>().health - damageAmount;
                other.GetComponent<Animator>().SetInteger("GetHitIndex", Random.Range(0, 2));
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
