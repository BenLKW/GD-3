using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCScript : MonoBehaviour
{
    public string npcName;
    public GameObject TextName;
    public GameObject Player;


    public DamageDealer doDamage;
    public GameObject Quest;


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
        transform.LookAt(Player.transform.position);
    }

    void GiveQuest()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Quest.SetActive(true);
            doDamage = GameObject.Find("pCube1").GetComponent<DamageDealer>();
        }
    }

}
