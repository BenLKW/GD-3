using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Animator animator;
    private NavMeshAgent enemy;
    public Transform player;
    public float lookRadius = 10f;
    public float range;
    public Transform centrePoint;
    public float speed;


    void Start()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<NavMeshAgent>();
        player = GameObject.Find("/Player_Test/Player").GetComponent<Transform>();
        centrePoint = GameObject.Find("/EnemySpawner/Center").GetComponent<Transform>();
    }

    void Update()
    {
        Attack();
        speed=enemy.speed  ;


        if (enemy.remainingDistance <= enemy.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point))
            {
                //Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); 
                enemy.SetDestination(point);
                animator.SetBool("Walk", true);
            }
        }
        
    }
    private void Attack()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance < lookRadius)
        {
            //transform.LookAt(player);
            //GetComponent<Rigidbody>().AddForce(transform.forward * speed);
            enemy.SetDestination(player.position);
            animator.SetBool("Walk", true);
        }

        if (distance <= enemy.stoppingDistance)
        {
            speed = 0;
            animator.SetBool("Walk", false);
        }
        else
        {
            speed = 2.5f;
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
