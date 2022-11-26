using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCScript : MonoBehaviour
{
    public string npcName;
    public GameObject TextName;
    public GameObject player;
    public GameObject NPCGUI,NPCGUIText,Startbar,Endbar;
    
    public float lookRadius = 10f;

    
    public string Name;

    [TextArea(5, 10)]
    public string[] sentences;

    public LayerMask whatIsPlayer;

    public GameObject Quest;
    public Quest quest;
    public bool isQuesting;
    public bool playerInLookingRadius;
    public void Start()
    {
        quest = GameObject.Find("Quest").GetComponent<Quest>();
        
        isQuesting = false;
        Endbar.SetActive(false);
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
        if (NPCGUI!=null)
        {
            //NPCGUIText.transform.localPosition=new Vector3(0,0,0);
            NPCGUIText.transform.LookAt(Camera.main.transform.position);
            NPCGUIText.transform.Rotate(0, 180, 0);
        }

        playerInLookingRadius = Physics.CheckSphere(transform.position, lookRadius, whatIsPlayer);

        if (playerInLookingRadius)
        {
            NPCGUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                Startbar.SetActive(true);
            }

        }
        else if (!playerInLookingRadius)
        {
            NPCGUI.SetActive(false);
        }



        RotateToPlayer();
        StartQuesting();
    }


    void RotateToPlayer()
    {
       
    }
    

    public void OnTriggerExit()
    {
       
    }
    public void GiveQuest()
    {
        if(isQuesting==false)
        {
            Debug.Log("Quest Start");
            isQuesting = true;
        }
       
            
           
    }

    void StartQuesting()
    {
        if (isQuesting == true)
        {
            quest = GameObject.Find("Quest").GetComponent<Quest>();

        }
    }
}
