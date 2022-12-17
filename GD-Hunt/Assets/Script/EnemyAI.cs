using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Animator animator;

    private NavMeshAgent enemy;
    
    public Transform player;
    public Health playerHealth;

    public DamageDealer damageDealer;

    public EnemyHealth enemyHealth;
    public enemyAttackDetector attackDetector;
    public FieldOfView fov;
    public PlayerMovement playerMovement;
   

    public LayerMask whatIsPlayer;

    public int damageAmount = 1;

    public float range;
    public Transform centrePoint;

    public float timeBetweenAttacks;
    public bool alreadyAttacked;
    

    public float lookRadius = 10f;
    public float chaseRadius = 8f;
    public float attackRadius = 2f;
    public bool playerInLookRadius, playerInAttackRadius,lowHealth,isDead;



    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
        fov = GetComponentInChildren<FieldOfView>();
        player = null;
        
        centrePoint = GameObject.Find("/EnemySpawner/Center").GetComponent<Transform>();
        
    }

    private void Update()
    {
        damageDealer = GetComponent<DamageDealer>();
        playerInLookRadius = Physics.CheckSphere(transform.position, lookRadius, whatIsPlayer);
        playerInAttackRadius = Physics.CheckSphere(transform.position, attackRadius, whatIsPlayer);
        animator.SetFloat("Speed", enemy.velocity.magnitude);
        if (!playerInLookRadius && !playerInAttackRadius) 
        {
            Patroling();
            player = null;
            playerMovement = null;
            
        }
        if (fov.canSeePlayer)
        {

        }

        if (playerInLookRadius && !playerInAttackRadius && !isDead)
        {
            
            AssignTarget();

            if (player != null)
            {
                Chase();
                
            }
            
        }

        if (!isDead && playerInAttackRadius && playerInLookRadius) 
        {

            AssignTarget();
            if (player != null)
            {
                Attack();
                
            }
        }
       


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
        
        
        enemy.SetDestination(player.position);
       
     
    }
    private void Attack()
    {

        
        enemy.SetDestination(player.position);
        
        Vector3 lookPos = player.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.1f*Time.deltaTime);

       


        if (!alreadyAttacked)
        {
            
            
            animator.SetTrigger("Attack");
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            
        }
        if (enemyHealth.health <= 3)
        {
            timeBetweenAttacks = 1f;
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        
    }

    
    public void StartDealDamage()
    {
        GetComponentInChildren<enemyAttackDetector>().StartDealDamage();
    }
    public void EndDealDamage()
    {
        GetComponentInChildren<enemyAttackDetector>().EndDealDamage();
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

    private GameObject ClosestTarget() // this is modified func from unity Docs ( Gets Closest Object with Tag ). 
    {
        GameObject[] Players;
        Players = GameObject.FindGameObjectsWithTag("Player");
        Vector3 position = transform.position;
        GameObject closet = null;

        foreach (GameObject Player in Players)
        {
            Vector3 diff = Player.transform.position - position;
            float curDistance = diff.magnitude;
            if (curDistance < lookRadius)
            {
                closet = Player;

            }
        }
        return closet;
    }

    private void AssignTarget()
    {
        if (ClosestTarget() != null)
        {
            player = ClosestTarget().GetComponent<Transform>();
            playerMovement = ClosestTarget().GetComponent<PlayerMovement>();
        }
            
        
        
    }
}
