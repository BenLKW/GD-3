using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public PlayerMovement PM;
    public GameObject GameOverUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PM.HealStage == PlayerMovement.HealthState.Dead)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GameOverUI.SetActive(true);
            
        }
            
    }

  public void ResetTheGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }
}
