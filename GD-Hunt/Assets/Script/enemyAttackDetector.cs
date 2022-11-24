using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttackDetector : MonoBehaviour
{
    public EnemyAI enemyAI;
    public bool isAttack;
    public Health playerHealth;
    Animator animator;


    private void Awake()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (enemyAI.alreadyAttacked&&other.gameObject.tag == "Player")
        {
            playerHealth = other.GetComponent<Health>();
            animator = other.GetComponent<Animator>();

            playerHealth.health--;
            animator.SetTrigger("GetHit");

        }
    }
}
