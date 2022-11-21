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
    public bool playerInRunAwayRadius;
    private void Awake()
    {

        animal = GetComponent<NavMeshAgent>();
        player = GameObject.Find("/Player_Test/Player").GetComponent<Transform>();
        centrePoint = GameObject.Find("/AnimalSpawner/Center").GetComponent<Transform>();
    }


    void Update()
    {

        playerInRunAwayRadius = Physics.CheckSphere(transform.position, lookRadius, whatIsPlayer);

        if (!playerInRunAwayRadius) Patroling();
        if (playerInRunAwayRadius) RunAwayFromPlayer();




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
    private void RunAwayFromPlayer()
    {
       
        
     Vector3 dirToPlayer = transform.position - player.transform.position;

     Vector3 newPos = transform.position + dirToPlayer;
       
     animal.SetDestination(newPos);       
              
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
    


}
