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
using UnityEngine.UI;
public class MainMenuScript : MonoBehaviour
{

    public GameObject buttonContianer; 
    public GameObject playButton;
    public GameObject settingsMenu;
    public GameObject statsButton;
    public GameObject helpButton;
    public GameObject creditsMenu;
    public GameObject quitButton;
    public GameObject confirmExitButton;

    public int playSceneNumber;
    private void Start(){
        InputEvents.Instance.PauseStarted.AddListener(escPressed);
    }
    public void escPressed()
    {
        if (settingsMenu.activeSelf) {
            settingsMenu.SetActive(false);
            buttonContianer.SetActive(true);
        } else if (creditsMenu.activeSelf) {
            settingsMenu.SetActive(false);
            buttonContianer.SetActive(true);
        } else if (confirmExitButton.activeSelf){
            buttonContianer.SetActive(true);
            confirmExitButton.SetActive(false);
        } else {
            buttonContianer.SetActive(false);
            confirmExitButton.SetActive(true);
        }
    }
    public void playClicked()
    {
        print("Loading Game...");
        SceneManager.LoadScene(playSceneNumber);
        print("Entered play scene");
    }
    public void settingsClicked()
    {
        settingsMenu.SetActive(true);
        buttonContianer.SetActive(false);
    }
    public void statsClicked()
    {
        print("Stats clicked");
    }
    public void helpClicked()
    {
        print("Help clicked");
    }
    public void creditsClicked()
    {
        creditsMenu.SetActive(true);
        buttonContianer.SetActive(false);
    }
    public void quitClicked()
    {
        confirmExitButton.SetActive(true);
        buttonContianer.SetActive(false);
    }
    public void confirmExitClicked()
    {
        print("Exiting Game...");
        Application.Quit();
    }
}
