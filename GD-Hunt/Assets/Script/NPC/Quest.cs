using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Quest : MonoBehaviour


{
    public int currentAmount;
    public int requiredAmount;
    public NPCScript npcScript;
    public GameObject quessProgress;



    public void Awake()
    {
        
    }
    public void Update()
    {
        npcScript = GameObject.Find("NPC_Test").GetComponent<NPCScript>();
        QuestCompleted();
       
    }
    
    
    public void QuestCompleted()
    {
        if (currentAmount >= requiredAmount)
        {
            npcScript.isQuesting = false;
            npcScript.Startbar.SetActive(false);
            quessProgress.SetActive(false);
            npcScript.Endbar.SetActive(true);
            
            Invoke("Endbargone", 2f);
            currentAmount = 0;
            Debug.Log("Quest End");
        }
    }

    void Endbargone()
    {
        npcScript.Endbar.SetActive(false);
    }
}
