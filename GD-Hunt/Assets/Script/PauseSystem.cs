using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseSystem : MonoBehaviour
{
    public static bool GameIsPaused;
    public GameObject PauseMenuUI;
    public GameObject PlayerUI;

    // Start is called before the first frame update
    void Start()
    {
        
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
       
    }

    public void Resume()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
