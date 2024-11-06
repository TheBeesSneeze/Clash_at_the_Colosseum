/*******************************************************************************
 * File Name :         DeathScren
 * Author(s) :         Clare G
 * Creation Date :     11/52024
 *
 * Brief Description : When player dies load up death screen
 *                     
 *****************************************************************************/
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class DeathScreen : MonoBehaviour
{

    //need to add Background music stuff simmilar to PauseMenu

    [SerializeField] private CanvasGroup deathGroup;
    [SerializeField] private Button respawnButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private BackgroundMusicManager backgroundManager;

    [Scene]
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    void Start()
    {
        PublicEvents.OnPlayerDeath.AddListener(showScreen);
        respawnButton.onClick.AddListener(Respawn);
        mainMenuButton.onClick.AddListener(goToMain); 

        if(backgroundManager == null)
        {
            backgroundManager = FindObjectOfType<BackgroundMusicManager>();
        }
    }


    private void showScreen()
    {
        if(backgroundManager.audioSourcePlayingCurrent)
        {
            backgroundManager.audioSource.Stop();
        }
        else
        {
            backgroundManager.secondaryAudio.Stop();
        }
        GameManager.Instance.isPaused = !GameManager.Instance.isPaused;
        TogglePauseUI(GameManager.Instance.isPaused);
        Cursor.visible = GameManager.Instance.isPaused;
        Cursor.lockState = GameManager.Instance.isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Time.timeScale = GameManager.Instance.isPaused ? 0 : 1;
    }

    private void Respawn()
    {
        SetDeathState(false);
        if (backgroundManager.audioSourcePlayingCurrent)
        {
            backgroundManager.secondaryAudio.Play();
        }
        else
        {
            backgroundManager.secondaryAudio.Play();
        }
        PublicEvents.OnPlayerRespawn.Invoke();
    }

    private void goToMain()
    {
        SaveDataManager.Instance.OnApplicationQuit();
        SceneManager.LoadScene(mainMenuSceneName);
    }

    private void TogglePauseUI(bool toggle)
    {
        deathGroup.alpha = toggle ? 1 : 0;
        deathGroup.interactable = toggle;
        deathGroup.blocksRaycasts = toggle;
    }

    public void SetDeathState(bool state)
    {
        GameManager.Instance.isPaused = state;
        TogglePauseUI(state);
        Cursor.visible = state;
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        Time.timeScale = state ? 0 : 1;
    }
}
