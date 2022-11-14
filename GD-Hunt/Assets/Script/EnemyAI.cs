using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Animator animator;

    private NavMeshAgent enemy;

    public Transform player;

    public LayerMask whatIsPlayer;

    public float range;
    public Transform centrePoint;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float lookRadius = 10f;
    public float attackRadius = 3f;
    public bool playerInLookRadius, playerInAttackRadius;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<NavMeshAgent>();
        player = GameObject.Find("/Player_Test/Player").GetComponent<Transform>();
        centrePoint = GameObject.Find("/EnemySpawner/Center").GetComponent<Transform>();
        animator.SetBool("Walk", true);
    }

    private void Update()
    {
        playerInLookRadius = Physics.CheckSphere(transform.position, lookRadius, whatIsPlayer);
        playerInAttackRadius = Physics.CheckSphere(transform.position, attackRadius, whatIsPlayer);

        if (!playerInLookRadius && !playerInAttackRadius) Patroling();
        if (playerInLookRadius && !playerInAttackRadius) Chase();
        if (playerInAttackRadius && playerInLookRadius) Attack();

        

    }
    private void Patroling()
    {
        if (enemy.remainingDistance <= enemy.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point))
            {

                enemy.SetDestination(point);

            }
        }
    }

    private void Chase()
    {

        animator.SetBool("Walk", true);
        enemy.SetDestination(player.position);
       
     
    }
    private void Attack()
    {
        
        animator.SetBool("Walk", false);
        //transform.LookAt(player);
        enemy.SetDestination(player.position);
        



        if (!alreadyAttacked)
        {

            Debug.Log("Attack!!");
            animator.SetTrigger("Attack");

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        //animator.SetBool("Attack", false);
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
