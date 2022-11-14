using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalAI : MonoBehaviour
{
    public NavMeshAgent animal;
    public Transform player;

    public float range;
    public Transform centrePoint;

    public LayerMask whatIsPlayer;

    public float lookRadius;
    public float runAwayRadius;
    public bool playerInLookRadius, playerInAttackRadius;
    private void Awake()
    {

        animal = GetComponent<NavMeshAgent>();
        player = GameObject.Find("/Player_Test/Player").GetComponent<Transform>();
        centrePoint = GameObject.Find("/AnimalSpawner/Center").GetComponent<Transform>();
    }


    void Update()
    {



        playerInLookRadius = Physics.CheckSphere(transform.position, lookRadius, whatIsPlayer);
        playerInAttackRadius = Physics.CheckSphere(transform.position, runAwayRadius, whatIsPlayer);

        Patroling();
        RunAwayFromPlayer();





    }


    private void Patroling()
    {
        if (animal.remainingDistance <= animal.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point))
            {

                animal.SetDestination(point);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
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
    private void RunAwayFromPlayer()
    {
        float distance = Vector3.Distance(player.position, transform.position);


        if (distance < lookRadius)
        {
            Vector3 dirToPlayer = transform.position - player.transform.position;

            Vector3 newPos = transform.position + dirToPlayer;

            animal.SetDestination(newPos);
        }
    }


}
