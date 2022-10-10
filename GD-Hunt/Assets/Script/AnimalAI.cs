using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalAI : MonoBehaviour
{
    public NavMeshAgent animal;
    public Transform player;
    public float lookRadius = 10f;
    public float range;
    public Transform centrePoint;

    private void Awake()
    {
        
        animal = GetComponent<NavMeshAgent>();
    }


    void Update()
    {

        RunAwayFromPlayer();


        if (animal.remainingDistance <= animal.stoppingDistance) 
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point)) 
            {
                //Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); 
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

        //Debug.Log("Animal Distance:" + distance);
        if (distance < lookRadius)
        {
            Vector3 dirToPlayer = transform.position - player.transform.position;

            Vector3 newPos = transform.position + dirToPlayer;

            animal.SetDestination(newPos);
        }
    }


}
