using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestNav : MonoBehaviour
{
    [SerializeField] private Transform movePositionTransform;

    NavMeshAgent em;

    private void Awake()
    {
        em = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        em.destination = movePositionTransform.transform.position;
    }
}
