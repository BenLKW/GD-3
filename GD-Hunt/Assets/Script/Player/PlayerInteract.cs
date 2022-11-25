using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public PlayerMovement PM;

    private void Start()
    {
        PM = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)){
            float interactRange = 5f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                if(collider.TryGetComponent(out NPCScript npc))
                {
                    npc.GiveQuest();
                }

                PM.TotalThrow = 20;
            }
        }
    }
}
