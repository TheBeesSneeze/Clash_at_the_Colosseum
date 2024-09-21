/*******************************************************************************
 * File Name :         PauseMenuToggle.cs
 * Author(s) :         Tyler
 * Creation Date :     9/3/2024
 *
 * Brief Description : Listens for the ESC key to be pressed and then brings 
 *                     up the pause menu.
 *****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseButtonsContainer;
    public GameObject resumeButton;
    public GameObject settingsMenu;
    public GameObject mainMenuButton;
    public GameObject backButton;
    
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private void Start()
    {
        InputEvents.Instance.PauseStarted.AddListener(escPressed);
    }
    public void escPressed() {

        if (GameManager.Instance.pausedForUI)
            return;

        if (settingsMenu.activeSelf){
            settingsMenu.SetActive(false);
            pauseButtonsContainer.SetActive(true);
            return;
        } else {
            GameManager.Instance.isPaused = !GameManager.Instance.isPaused;

            pauseButtonsContainer.SetActive(GameManager.Instance.isPaused);
        }

        //sorry for weird refactor, i was getting a bug where the cursor wasnt hiding
        Cursor.visible = GameManager.Instance.isPaused;
        Cursor.lockState = GameManager.Instance.isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Time.timeScale = GameManager.Instance.isPaused ? 0 : 1;
        
    }
    public void SettingsClicked() {
        pauseButtonsContainer.SetActive(false);
        settingsMenu.SetActive(true);
    }
    public void mainMenuClicked() {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
