using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    public PlayerMovement PM;
    public Text RockNumbers;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RockNumbers.text = PM.TotalThrow.ToString();
    }
}
