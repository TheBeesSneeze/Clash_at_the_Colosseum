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
    public GameObject settingsButton;
    public GameObject statsButton;
    public GameObject helpButton;
    public GameObject quitButton;

    public GameObject settingsMenu;
    public GameObject backButton;
    public GameObject volumeSlider;
    public GameObject sensitivitySlider;
    public GameObject inputSwitch;
    public GameObject inputSwitchText;

    public GameObject wantToQuitButton;

    public int playSceneNumber;
    private void Start(){
        InputEvents.Instance.PauseStarted.AddListener(escPressed);
    }
    public void escPressed()
    {
        if (settingsMenu.activeSelf){
            settingsMenu.SetActive(false);
            buttonContianer.SetActive(true);
        } else if (wantToQuitButton.activeSelf){
            buttonContianer.SetActive(true);
            wantToQuitButton.SetActive(false);
        } else {
            buttonContianer.SetActive(false);
            wantToQuitButton.SetActive(true);
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
    public void quitClicked()
    {
        buttonContianer.SetActive(false);
        wantToQuitButton.SetActive(true);
    }
    public void wantToQuitClicked()
    {
        print("Exiting Game...");
        Application.Quit();
    }
    public void inputSwitchClicked()
    {
        Text inputbuttonText = inputSwitchText.GetComponent<Text>();
        if (inputbuttonText.text == "Input: Keyboard"){
            inputbuttonText.text = "Input: Controller";
        } else {
            inputbuttonText.text = "Input: Keyboard";
        }
    }
    public void volumeChanged()
    {
        Slider slider = volumeSlider.GetComponent<Slider>();
        float sliderValue = slider.value;
        print("Volume: " + Mathf.Round(sliderValue * 100f) + "%");
    }
    public void sensitivityChanged()
    {
        Slider slider = sensitivitySlider.GetComponent<Slider>();
        float sliderValue = slider.value;
        print("Sensitivity: " + Mathf.Round(sliderValue * 100f) + "%");
    }
}
