using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttackDetector : MonoBehaviour
{
    public EnemyAI enemyAI;
    public bool isAttack;
    

    public void OnTriggerEnter(Collider other)
    {
        if (enemyAI.alreadyAttacked==true&&other.tag == "Player")
        {
            isAttack = true;
        }
        else
        {
            isAttack = false;
        }
    }
}
