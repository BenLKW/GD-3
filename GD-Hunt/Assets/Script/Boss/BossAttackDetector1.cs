using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackDetector1 : MonoBehaviour
{
    public BossAI bossAI;

    public Health playerHealth;

    public Collider AttackDetactor;

    public bool canDealDamage;
    public bool hasDealDamage;
    [SerializeField] float weaponLength;
    private void Start()
    {
        AttackDetactor = GetComponent<Collider>();

        //canDealDamage = false;
        AttackDetactor.enabled = false;

    }
    public void StartDealDamage()
    {
        //canDealDamage = true;
        AttackDetactor.enabled = true;

    }
    public void EndDealDamage()
    {
        //canDealDamage = false;
        AttackDetactor.enabled = false;
        hasDealDamage = false;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (hasDealDamage == false)
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
        playerHealth.TakeDamage(5);

    }




}
