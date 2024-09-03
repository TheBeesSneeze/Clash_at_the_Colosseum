/*******************************************************************************
 * File Name :         MainMenuScript.cs
 * Author(s) :         Tyler
 * Creation Date :     9/3/2024
 *
 * Brief Description : controls the actions for the main menu buttons
 *****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour
{
    [Tooltip("Main menu object goes here")]
    public GameObject pauseMenu;

    public GameObject playButton;
    public GameObject settingsButton;
    public GameObject statsButton;
    public GameObject helpButton;
    public GameObject quitButton;

    public bool isPaused = false;
    public int mainMenuSceneNumber = 0;
    public void playClicked()
    {
        print("Play clicked");
    }
    public void settingsClicked()
    {
        print("Settings clicked");
    }
    public void statsClicked()
    {
        print("Stats clicked");
    }
    public void helpClicked()
    {
        print("Help clicked");
    }
    public void quitClicked()
    {
        Application.Quit();
    }
}
