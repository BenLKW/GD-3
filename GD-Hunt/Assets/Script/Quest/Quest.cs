using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Quest : NPCScript
{
    public int currentAmount;
    public int requiredAmount;
    

    

    public void Update()
    {
       
        QuestCompleted();
       
    }
    
    
    public void QuestCompleted()
    {
        if (currentAmount >= requiredAmount)
        {
            
            currentAmount = 0;
            Debug.Log("Quest End");
        }
    }


}
