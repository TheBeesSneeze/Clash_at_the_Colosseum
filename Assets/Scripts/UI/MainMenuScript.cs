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
    public GameObject playButton;
    public int playSceneNumber;
    public GameObject settingsButton;
    public GameObject statsButton;
    public GameObject helpButton;
    public GameObject quitButton;
    public void playClicked()
    {
        print("Loading Game...");
        SceneManager.LoadScene(playSceneNumber);
        print("Entered play scene");
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
        print("Exiting Game...");
        //Save the stats you want to save here
        Application.Quit();
    }
}
