using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalAI : MonoBehaviour
{
    private NavMeshAgent animal;
    public Transform player;
    public float lookRadius = 10f;
    

    void Start()
    {
        animal = GetComponent<NavMeshAgent>();
    }


    void Update()
    {


        float distance = Vector3.Distance(player.position, transform.position);

        Debug.Log("Animal Distance:" + distance);
        if (distance < lookRadius)
        {
            Vector3 dirToPlayer = transform.position - player.transform.position;

            Vector3 newPos = transform.position + dirToPlayer;

            animal.SetDestination(newPos);
        }


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
    
}
