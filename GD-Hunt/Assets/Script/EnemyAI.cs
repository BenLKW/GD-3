using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent enemy;
    public Transform player;
    public float lookRadius = 10f;
    public float range;
    public Transform centrePoint;


    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        Attack();



        if (enemy.remainingDistance <= enemy.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point))
            {
                //Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); 
                enemy.SetDestination(point);
            }
        }

    }
    private void Attack()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance < lookRadius)
        {
            enemy.SetDestination(player.position);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {

            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
