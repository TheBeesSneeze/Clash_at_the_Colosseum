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
    
    public GameManager gameManager;
    public int mainMenuSceneNumber = 0;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        InputEvents.Instance.PauseStarted.AddListener(escPressed);
    }
    public void escPressed() {
        if (settingsMenu.activeSelf){
            settingsMenu.SetActive(false);
            pauseButtonsContainer.SetActive(true);
        } else {
            gameManager.isPaused = !gameManager.isPaused;
            if (gameManager.isPaused){
                pauseButtonsContainer.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
            }else{
                pauseButtonsContainer.SetActive(false);
                Cursor.visible = false;
                Time.timeScale = 1;
            }
        }
    }
    public void SettingsClicked() {
        pauseButtonsContainer.SetActive(false);
        settingsMenu.SetActive(true);
    }
    public void mainMenuClicked() {
        SceneManager.LoadScene(mainMenuSceneNumber);
    }
}
