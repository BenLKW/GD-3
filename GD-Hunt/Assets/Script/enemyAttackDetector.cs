using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttackDetector : MonoBehaviour
{
    public EnemyAI enemyAI;
    public bool isAttack;
    public Health health;
    Animator animator;


    private void Awake()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            health = other.GetComponent<Health>();

            //health.TakeDamage(1);
        }
       
        
    }
    
}
