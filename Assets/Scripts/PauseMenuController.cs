using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenuController : MonoBehaviour
{
    //The Pause Menu object itself
    public GameObject wristUI;

    //Whether or not the menu is active
    public bool activeMenu = true;

    // Start is called before the first frame update
    void Start()
    {
        DisplayWristUI();
    }

    public void PauseButtonPressed(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            DisplayWristUI();
        }
    }

    public void DisplayWristUI()
    {
        if (activeMenu)
        {
            //Hide pause menu and restart the game
            wristUI.SetActive(false);
            activeMenu = false;
            Time.timeScale = 1;
        }
        else if (!activeMenu)
        {
            //Show Pause Menu and pause the game
            wristUI.SetActive(true);
            activeMenu = true;
            Time.timeScale = 0;
        }
    }

    //Button OnClick Functions
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenuBtn()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitBtn()
    {
        Application.Quit();
    }
}
