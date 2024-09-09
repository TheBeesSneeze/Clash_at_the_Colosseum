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
public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsMenu;
    [SerializeField] private Button statsButton;
    [SerializeField] private Button helpButton;
    [SerializeField] private Button creditsMenu;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button confirmExitButton;

    public string mainSceneName = "Gun Test";
    private void Start()
    {
        InputEvents.Instance.PauseStarted.AddListener(escPressed);
    }
    public void escPressed()
    {
        /*
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
        */
    }
    public void playClicked()
    {
        Debug.Log("Loading Game...");
        SceneManager.LoadScene(mainSceneName);
        Debug.Log("Entered play scene");
    }
    public void settingsClicked()
    {
    }
    public void statsClicked()
    {
        Debug.Log("Stats clicked");
    }
    public void helpClicked()
    {
        Debug.Log("Help clicked");
    }
    public void creditsClicked()
    {
    }
    public void quitClicked()
    {
    }
    public void confirmExitClicked()
    {
        Debug.Log("Exiting Game...");
        Application.Quit();
    }
}
