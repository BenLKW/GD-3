using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent enemy;
    public Transform player;
    public Transform centrePoint;

    public float lookRadius = 10f;
    public float range;
    Renderer render;
    

    public Material[] myMaterial;
    public int x;

    bool functionCalled = false;

    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();

        x = 0;
        render = GetComponent<Renderer>();
        render.enabled = true;
        render.sharedMaterial = myMaterial[x];

        
    }


    void Update()
    {
        x = 0;
        RunToPlayer();
        render.sharedMaterial = myMaterial[x];

        if (enemy.remainingDistance <= enemy.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                enemy.SetDestination(point);
            }
        }
        if (!functionCalled)
        {
            
            x++;
            functionCalled = true;
        }
        

    }

    void Attack()
    {

        Debug.Log("Attack!!!");
        

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
     private void RunToPlayer()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance < lookRadius)
        {
           x++; 
           enemy.SetDestination(player.position);
            
        }
        if (distance < 3)
        {
            Attack();
        }
       
    }

}
