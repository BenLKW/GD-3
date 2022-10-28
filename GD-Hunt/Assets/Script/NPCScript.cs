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

    public void Start()
    {
        Quest.SetActive(false);
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
        GiveQuest();
    }


    void RotateToPlayer()
    {
        transform.LookAt(player.transform.position);
    }

    void GiveQuest()
    {
        
        if (Input.GetKeyDown(KeyCode.V))
        {
            questTrue = GameObject.Find("pCube1").GetComponent<DamageDealer>();
            Quest.SetActive(true);
            questTrue.isQuesting = true;
            Debug.Log("Start Quest");
            
        }
    }

}
