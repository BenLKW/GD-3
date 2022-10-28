using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Quest : MonoBehaviour


{
    public int currentAmount;
    public int requiredAmount;
    public DamageDealer questFalse;



    public void Update()
    {
        questFalse = GameObject.Find("pCube1").GetComponent<DamageDealer>();
        QuestCompleted();
       
    }
    
    
    public void QuestCompleted()
    {
        if (currentAmount >= requiredAmount)
        {
            questFalse.isQuesting = false;
            currentAmount = 0;
            Debug.Log("Quest End");
        }
    }


}
