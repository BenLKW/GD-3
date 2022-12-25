using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCScript1 : MonoBehaviour
{
    public string npcName;
    public GameObject TextName;
    public GameObject player;
    public GameObject NPCGUI,NPCGUIText,Startbar;

    public Transform PressF; 
    public float lookRadius = 10f;

    
    public string Name;

    [TextArea(5, 10)]
    public string[] sentences;

    public LayerMask whatIsPlayer;

    
    public bool playerInLookingRadius;
    public void Start()
    {
        
        
        PressF.transform.position = new Vector3(this.transform.position.x,this.transform.position.y+1f,this.transform.position.z);
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
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

        }
        else if (!playerInLookingRadius)
        {
            NPCGUI.SetActive(false);
        }



       
        
    }


   
    
}
