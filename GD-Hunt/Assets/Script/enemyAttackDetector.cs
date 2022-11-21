using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttackDetector : MonoBehaviour
{
    public EnemyAI enemyAI;
    public bool isAttack;
    public Health playerHealth;


    private void Awake()
    {
        playerHealth = GameObject.Find("Player").GetComponent<Health>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (enemyAI.alreadyAttacked&&other.gameObject.tag == "Player")
        {

            playerHealth.health--;
        }
    }
}
