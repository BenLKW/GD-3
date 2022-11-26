using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Quest : MonoBehaviour


{
    public int currentAmount;
    public int requiredAmount;
    public NPCScript questFalse;



    public void Update()
    {
        questFalse = GameObject.Find("NPC_Test").GetComponent<NPCScript>();
        QuestCompleted();
       
    }
    
    
    public void QuestCompleted()
    {
        if (currentAmount >= requiredAmount)
        {
            questFalse.isQuesting = false;
            questFalse.Startbar.SetActive(false);
            questFalse.Endbar.SetActive(true);
            Invoke("Endbargone", 2f);
            currentAmount = 0;
            Debug.Log("Quest End");
        }
    }

    void Endbargone()
    {
        questFalse.Endbar.SetActive(false);
    }
}
