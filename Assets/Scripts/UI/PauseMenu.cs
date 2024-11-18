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
using NaughtyAttributes;
using Managers;

namespace UI
{
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private BackgroundManager backgroundManager;

    [SerializeField] private CanvasGroup pauseGroup;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button restartGameButton;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private Tutorialmusic tutorialMusic;

    [Scene]
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private float baseBGMVolume;
    private bool canPressEscape = true;

    private void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 1);
        sensitivitySlider.value = PlayerPrefs.GetFloat("sensitivity", GameManager.Instance.DefaultSensitivity);

        if (backgroundManager == null)
        {
            Debug.LogWarning("no background music set.");
            backgroundManager = FindObjectOfType<BackgroundManager>();
        }

        if (tutorialMusic == null)
        {
            tutorialMusic = FindObjectOfType<Tutorialmusic>();
        }

        if (tutorialMusic != null)
        {
            tutorialMusic.StartMusic();
        }

        if (backgroundManager != null)
        {
            baseBGMVolume = backgroundManager.audioSource.volume;
            Debug.Log("baseBGM: " + baseBGMVolume);
            backgroundManager.audioSource.volume = baseBGMVolume * volumeSlider.value;
            backgroundManager.volumeSliderAdjustment = volumeSlider.value;
        }
        TogglePauseUI(false);

        InputEvents.Instance.PauseStarted.AddListener(escPressed);

        resumeButton.onClick.AddListener(ResumeClicked);
        mainMenuButton.onClick.AddListener(mainMenuClicked);
        restartGameButton.onClick.AddListener(RestartGame);
        volumeSlider.onValueChanged.AddListener(volumeChanged);
        sensitivitySlider.onValueChanged.AddListener(sensitivityChanged);

        PublicEvents.StartSound.Invoke();
        PublicEvents.OnPlayerDeath.AddListener(SetCanEsc);
        PublicEvents.OnPlayerRespawn.AddListener(SetCanEsc);
    }
    public void escPressed()
    {
        if (canPressEscape)
        {
            Debug.Log("pause");

            if (GameManager.Instance.pausedForUI)
                return;

            if (backgroundManager != null)
            {
                if (backgroundManager.audioSourcePlayingCurrent)
                {
                    if (!GameManager.Instance.isPaused)
                    {
                        backgroundManager.audioSource.Pause();
                    }
                    else
                    {
                        backgroundManager.audioSource.Play();
                    }
                }
                else
                {
                    if (!GameManager.Instance.isPaused)
                    {
                        backgroundManager.secondaryAudio.Pause();
                    }
                    else
                    {
                        backgroundManager.secondaryAudio.Play();
                    }
                }
            }

            GameManager.Instance.isPaused = !GameManager.Instance.isPaused;
            TogglePauseUI(GameManager.Instance.isPaused);
            Cursor.visible = GameManager.Instance.isPaused;
            Cursor.lockState = GameManager.Instance.isPaused ? CursorLockMode.None : CursorLockMode.Locked;
            Time.timeScale = GameManager.Instance.isPaused ? 0 : 1;
        }

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
        if (backgroundManager != null)
        {
            if (backgroundManager.audioSourcePlayingCurrent)
            {
                backgroundManager.audioSource.Play();
            }
            else
            {
                backgroundManager.secondaryAudio.Play();
            }
        }
    }

    public void mainMenuClicked()
    {
        SaveDataManager.Instance.OnApplicationQuit();
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void volumeChanged(float value)
    {
        float sliderValue = value;
        backgroundManager.volumeSliderAdjustment = sliderValue;
        PlayerPrefs.SetFloat("volume", sliderValue);
        AudioManager.masterVolume = sliderValue;
        if (backgroundManager != null)
        {
            if (backgroundManager.audioSourcePlayingCurrent)
            {
                backgroundManager.audioSource.volume = sliderValue * baseBGMVolume;
            }
            else
            {
                backgroundManager.secondaryAudio.volume = sliderValue * baseBGMVolume;
            }
        }
        if (tutorialMusic != null)
        {
            tutorialMusic.volumeSliderAdjustment = sliderValue;
            tutorialMusic.UpdateVolume();
        }

        Debug.Log(sliderValue * baseBGMVolume);
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

    private void SetCanEsc()
    {
        canPressEscape = !canPressEscape;
    }
}
}

