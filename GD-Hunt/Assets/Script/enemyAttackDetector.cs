using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttackDetector : MonoBehaviour
{
    public EnemyAI enemyAI;

    

    public void OnTriggerEnter(Collider other)
    {
        if (enemyAI.alreadyAttacked&&other.tag == "Player")
        {
            Debug.Log("Attack!!");
        }
    }
}
