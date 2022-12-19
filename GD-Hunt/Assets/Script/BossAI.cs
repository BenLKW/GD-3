using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    public Animator anim;

    private NavMeshAgent boss;

    public Transform player;
    public PlayerMovement playerMovement;



    public float range;
    public Transform BossPatrollingCenter;

    

    public FieldOfView fov;
    public LayerMask whatIsPlayer;

    public float lookRadius = 10f;
    public bool playerInLookRadius;
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
        anim.SetFloat("Speed", boss.velocity.magnitude);
        if (!playerInLookRadius)
        {
            Patroling();
            player = null;
            playerMovement = null;

        }
        if (playerInLookRadius )
        {

            AssignTarget();

            if (player != null)
            {
                Chase();

            }

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
