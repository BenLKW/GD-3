using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    
    

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
         Debug.Log(other.name + "Hit");
         Destroy(other.gameObject);
         
        
            
        }
        if (other.tag == "Animal")
        {
            Debug.Log(other.name + "Hit");
            Destroy(other.gameObject);
        }
    }
}
