using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform player;
    public float lookRadius = 10f;


    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
    }


    void Update()
    {


        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= lookRadius)
        {
            enemy.SetDestination(player.position);

        }


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}
