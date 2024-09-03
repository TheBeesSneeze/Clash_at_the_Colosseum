/*******************************************************************************
 * File Name :         PauseMenuToggle.cs
 * Author(s) :         Tyler
 * Creation Date :     9/3/2024
 *
 * Brief Description : Gets put on the Canvas. Listens for the ESC key to be
 *                     pressed and then brings up the pause menu.
 *****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenuScript : MonoBehaviour
{
    [Tooltip("Pause menu object goes here")]
    public GameObject pauseMenu;

    public GameObject resumeButton;
    public GameObject settingsButton;
    public GameObject mainMenuButton;

    public bool isPaused = false;
    public int mainMenuSceneNumber = 0;
    private void Start(){
        InputEvents.Instance.PauseStarted.AddListener(togglePause);
    }
    public void togglePause() {
        isPaused = !isPaused;
        if (isPaused) {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        } else {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void SettingsClicked() {
        print("Settings Clicked");
    }
    public void mainMenuClicked() {
        SceneManager.LoadScene(mainMenuSceneNumber);
    }
}
