using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCScript : MonoBehaviour
{
    public string npcName;
    public GameObject TextName;
    public GameObject player;
    public GameObject NPCGUI;
    public float lookRadius = 10f;

    private DialogueSystem dialogueSystem;
    public string Name;

    [TextArea(5, 10)]
    public string[] sentences;


    public GameObject Quest;
    public Quest quest;
    public bool isQuesting;

    public void Start()
    {
        quest = GameObject.Find("Quest").GetComponent<Quest>();
        dialogueSystem = FindObjectOfType<DialogueSystem>();
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
        if (NPCGUI != null)
        {
            NPCGUI.transform.LookAt(Camera.main.transform.position);
            NPCGUI.transform.Rotate(0, 180, 0);
        }
        RotateToPlayer();
        StartQuesting();
    }


    void RotateToPlayer()
    {
        transform.LookAt(player.transform.position);
    }
    public void OnTriggerStay(Collider other)
    {
        //this.gameObject.GetComponent<NPCScript>().enabled = true;
        FindObjectOfType<DialogueSystem>().EnterRangeOfNPC();
        if ((other.gameObject.tag == "Player") && Input.GetKeyDown(KeyCode.F))
        {
            this.gameObject.GetComponent<NPCScript>().enabled = true;
            dialogueSystem.Names = Name;
            dialogueSystem.dialogueLines = sentences;
            FindObjectOfType<DialogueSystem>().NPCName();
        }
    }

    public void OnTriggerExit()
    {
        FindObjectOfType<DialogueSystem>().OutOfRange();
        this.gameObject.GetComponent<NPCScript>().enabled = false;
    }
    public void GiveQuest()
    {
        
            isQuesting = true;
            
           
    }

    void StartQuesting()
    {
        if (isQuesting == true)
        {
            quest = GameObject.Find("Quest").GetComponent<Quest>();

        }
    }
}
