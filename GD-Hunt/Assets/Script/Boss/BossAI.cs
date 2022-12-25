using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    public Animator anim;

    private NavMeshAgent boss;
    private BossHealth bossHealth;
    public Transform player;
    public PlayerMovement playerMovement;

    public Transform target;
    public float rotateSpeed;

    public float range;
    public Transform BossPatrollingCenter;

    public bool alreadyAttacked;
    public float timeBetweenAttacks;

    public FieldOfView fov,FrontDetector;
    public LayerMask whatIsPlayer;

    public float lookRadius = 10f;
    public float attackRadius = 2f;
    public bool playerInLookRadius, playerInAttackRadius,isDead;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        boss = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerInLookRadius = Physics.CheckSphere(transform.position, lookRadius, whatIsPlayer);
        playerInAttackRadius = Physics.CheckSphere(transform.position, attackRadius, whatIsPlayer);
        anim.SetFloat("Speed", boss.velocity.magnitude);
        if (!playerInLookRadius&!isDead)
        {
            Patroling();
            player = null;
            playerMovement = null;

        }
        if (playerInLookRadius && !playerInAttackRadius&!isDead)
        {

            AssignTarget();

            if (player != null)
            {
                Chase();

            }

        }
        if (playerInAttackRadius && playerInLookRadius&&fov.canSeePlayer&!isDead)
        {

            AssignTarget();
            if (player != null)
            {
                Attack();

            }

        }
        if (FrontDetector.canSeePlayer)
        {
            Debug.Log("Hello World");
        }
    }
    private void Chase()
    {
        boss.SetDestination(player.position);


    }
    private void Patroling()
    {

        if (boss.remainingDistance <= boss.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(BossPatrollingCenter.position, range, out point))
            {

                boss.SetDestination(point);

            }
        }
    }
    private void Attack()
    {


        boss.SetDestination(player.position);

        Quaternion rotTarget = Quaternion.LookRotation(target.position - this.transform.position);
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation,rotTarget,rotateSpeed*Time.deltaTime);




        if (!alreadyAttacked)
        {

            anim.SetTrigger("Attack");
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

        }
        

    }
    private void ResetAttack()
    {
        alreadyAttacked = false;

    }

    public void StartDealDamage()
    {

        
      GetComponentInChildren<BossAttackDetector>().StartDealDamage();
        

    }
    public void EndDealDamage()
    {
        
       GetComponentInChildren<BossAttackDetector>().EndDealDamage();
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
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
