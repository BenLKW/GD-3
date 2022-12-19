using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    public Animator anim;

    private NavMeshAgent boss;

    public float range;
    public Transform BossPatrollingCenter;

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
        }
        
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
