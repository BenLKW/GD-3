using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Quest : MonoBehaviour
{
    public int currentAmount;
    public int requiredAmount;
    public NPCScript npc;
    

    

    public void Start()
    {
        
    }
    public void Update()
    {
        
        QuestCompleted();
        Questing();
    }
    
    public void Questing()
    {
        
        if (npc.doDamage.enemyKilled)
        {
            currentAmount++;
        }
    }
    public void QuestCompleted()
    {
        if (currentAmount >= requiredAmount)
        {
            
            currentAmount = 0;
            Debug.Log("Great Job");
        }
    }


}
