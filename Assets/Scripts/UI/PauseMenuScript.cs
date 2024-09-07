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
    public GameObject settingsButton;
    public GameObject mainMenuButton;

    public GameObject settingsMenu;
    public GameObject backButton;
    public GameObject volumeSlider;
    public GameObject sensitivitySlider;

    public bool isPaused = false;
    public int mainMenuSceneNumber = 0;
    private void Start()
    {
        InputEvents.Instance.PauseStarted.AddListener(escPressed);
        Slider slider = volumeSlider.GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat("volume", 1);
        slider = sensitivitySlider.GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat("sensitivity", 1);
    }
    public void escPressed() {
        if (settingsMenu.activeSelf)
        {
            settingsMenu.SetActive(false);
            pauseButtonsContainer.SetActive(true);
        } else {
            isPaused = !isPaused;
            if (isPaused){
                pauseButtonsContainer.SetActive(true);
                Time.timeScale = 0;
            }else{
                pauseButtonsContainer.SetActive(false);
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
    public void volumeChanged()
    {
        Slider slider = volumeSlider.GetComponent<Slider>();
        PlayerPrefs.SetFloat("volume", slider.value);
        float sliderValue = slider.value;
    }
    public void sensitivityChanged()
    {
        Slider slider = sensitivitySlider.GetComponent<Slider>();
        PlayerPrefs.SetFloat("sensitivity", slider.value);
        float sliderValue = slider.value;
    }
}
