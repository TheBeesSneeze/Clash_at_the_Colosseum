/*******************************************************************************
 * File Name :         PauseMenuToggle.cs
 * Author(s) :         Tyler, Toby S
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
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;

    [SerializeField] private CanvasGroup pauseGroup;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button restartGameButton;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider sensitivitySlider;
    
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private float baseBGMVolume;

    private void Start()
    {
        InputEvents.Instance.PauseStarted.AddListener(escPressed);

        resumeButton.onClick.AddListener(ResumeClicked);
        mainMenuButton.onClick.AddListener(mainMenuClicked);
        restartGameButton.onClick.AddListener(RestartGame);
        volumeSlider.onValueChanged.AddListener(volumeChanged);
        sensitivitySlider.onValueChanged.AddListener(sensitivityChanged);

        volumeSlider.value = PlayerPrefs.GetFloat("volume", 1);
        sensitivitySlider.value = PlayerPrefs.GetFloat("sensitivity", 0.4f);

        if(backgroundMusic == null)
        {
            Debug.LogWarning("no background music set.");
            backgroundMusic = GameObject.FindObjectOfType<AudioSource>();
        }

        if (backgroundMusic != null)
        {
            baseBGMVolume = backgroundMusic.volume;
            backgroundMusic.volume = baseBGMVolume * AudioManager.masterVolume;
        }

        

        TogglePauseUI(false);
    }
    public void escPressed() {

        Debug.Log("pause");

        if (GameManager.Instance.pausedForUI)
            return;
        
        GameManager.Instance.isPaused = !GameManager.Instance.isPaused;
        TogglePauseUI(GameManager.Instance.isPaused);

        Cursor.visible = GameManager.Instance.isPaused;
        Cursor.lockState = GameManager.Instance.isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Time.timeScale = GameManager.Instance.isPaused ? 0 : 1;
        
    }

    public void SetPauseState(bool state)
    {
        GameManager.Instance.isPaused = state;
        TogglePauseUI(state);

        Cursor.visible = state;
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        Time.timeScale = state ? 0 : 1;
    }    

    private void TogglePauseUI(bool toggle)
    {
        pauseGroup.alpha = toggle ? 1 : 0;
        pauseGroup.interactable = toggle;
        pauseGroup.blocksRaycasts = toggle;
    }

    public void ResumeClicked()
    {
        SetPauseState(false);
    }

    public void mainMenuClicked() {
        SaveDataManager.Instance.OnApplicationQuit();
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void volumeChanged(float value)
    {
        float sliderValue = volumeSlider.value;
        PlayerPrefs.SetFloat("volume", sliderValue);
        AudioManager.masterVolume = sliderValue;

        if (backgroundMusic != null)
            backgroundMusic.volume = sliderValue * baseBGMVolume;
    }

    public void sensitivityChanged(float value)
    {
        float sliderValue = sensitivitySlider.value;
        PlayerPrefs.SetFloat("sensitivity", sliderValue);
        PublicEvents.OnSensitivitySliderChanged.Invoke();
    }

    public void RestartGame()
    {
        SaveDataManager.Instance.OnApplicationQuit();
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
