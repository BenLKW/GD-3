using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalAI : MonoBehaviour
{
    public NavMeshAgent animal;
    public Transform player;
    public float lookRadius = 10f;


    void Start()
    {
        animal = GetComponent<NavMeshAgent>();
    }


    void Update()
    {


        float distance = Vector3.Distance(-player.position, -transform.position);
        if (distance <= lookRadius)
        {
            animal.SetDestination(-player.position);

        }


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}
