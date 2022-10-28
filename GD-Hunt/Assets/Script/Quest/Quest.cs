using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Quest : MonoBehaviour
{
    public int currentAmount;
    public int requiredAmount;
    

    

    public void Start()
    {
        
    }
    public void Update()
    {
       
        QuestCompleted();
       
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
