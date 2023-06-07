using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
   

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ScreenCapture.CaptureScreenshot("Screenshot.png");

        }    
    }
}
