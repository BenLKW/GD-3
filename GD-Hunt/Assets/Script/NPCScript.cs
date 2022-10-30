using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCScript : MonoBehaviour
{
    public string npcName;
    public GameObject TextName;
    public GameObject player;
    public float lookRadius = 10f;
    public GameObject Quest;
    public DamageDealer questTrue;
    public Quest quest;
    public bool isQuesting;

    public void Start()
    {
        quest = GameObject.Find("Quest").GetComponent<Quest>();

        isQuesting = false;
    }
    private void Update()
    {
        if(TextName != null)
        {
            TextName.transform.LookAt(Camera.main.transform.position);
            TextName.transform.Rotate(0, 180, 0);
            TextName.GetComponent<TextMesh>().color = Color.green;
            TextName.GetComponent<TextMesh>().text = "" + npcName;
        }
        RotateToPlayer();
        StartQuesting();
    }


    void RotateToPlayer()
    {
        transform.LookAt(player.transform.position);
    }

    public void GiveQuest()
    {
        
        
        
            
            
            isQuesting = true;
            Debug.Log("Start Quest");
            
        
    }

    void StartQuesting()
    {
        if (isQuesting == true)
        {
            quest = GameObject.Find("Quest").GetComponent<Quest>();

        }
    }
}
