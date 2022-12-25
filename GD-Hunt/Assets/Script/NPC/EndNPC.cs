using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndNPC : MonoBehaviour
{
    //Self
    public string npcName;
    public GameObject endNPC;
    public GameObject TextName;
    public GameObject NPCGUI, NPCGUIText, EndNPCBar;

    public Transform PressF;
    public float lookRadius = 10f;
    public LayerMask whatIsPlayer;
    //Boss
    public Transform elephant;
    public BossAI bossAI;

    public bool playerInLookingRadius;
    // Start is called before the first frame update
    void Start()
    {
        endNPC = GameObject.Find("EndNPC");
        endNPC.transform.position=new Vector3(0, 0, 0);
        bossAI = GameObject.Find("elephant").GetComponent<BossAI>();
        elephant = GameObject.Find("elephant").GetComponent<Transform>();
        //TextName.SetActive(false);
    }

    // Update is called once per frame
    public void Update()
    {
        playerInLookingRadius = Physics.CheckSphere(transform.position, lookRadius, whatIsPlayer);

        if (bossAI.isDead==true)
        {
            endNPC.transform.position = new Vector3(elephant.transform.position.x+20, 0, elephant.transform.position.z + 20);
            PressF.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z);
            //TextName.SetActive(true);
        }

        if (TextName != null)
        {
            TextName.transform.LookAt(Camera.main.transform.position);
            TextName.transform.Rotate(0, 180, 0);
            TextName.GetComponent<TextMesh>().color = Color.green;
            TextName.GetComponent<TextMesh>().text = "" + npcName;
        }
        if (NPCGUI != null)
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
                EndNPCBar.SetActive(true);
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
