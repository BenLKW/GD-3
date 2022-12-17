using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    public PlayerMovement PM;
    public Text RockNumbers;
    public Text AidNumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RockNumbers.text = PM.TotalThrow.ToString();
        AidNumber.text = PM.TotalAid.ToString();

        if(PM.Item == PlayerMovement.WhichItem.Stone || PM.Item== PlayerMovement.WhichItem.Rope)
        {
            RockNumbers.enabled = true;
            AidNumber.enabled = false;
        }
        else if (PM.Item == PlayerMovement.WhichItem.Aid)
        {
            AidNumber.enabled = true;
            RockNumbers.enabled = false;
        }
    }
}
