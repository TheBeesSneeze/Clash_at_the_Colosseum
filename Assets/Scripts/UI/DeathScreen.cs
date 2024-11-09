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
    [SerializeField] private BackgroundManager backgroundManager;

    [Scene]
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    void Start()
    {
        PublicEvents.OnPlayerDeath.AddListener(showScreen);
        respawnButton.onClick.AddListener(Respawn);
        mainMenuButton.onClick.AddListener(goToMain);

        ToggleDeathUI(false);

        if (backgroundManager == null)
            backgroundManager = FindObjectOfType<BackgroundManager>();

    }


    private void showScreen()
    {
        GameManager.Instance.isPaused = !GameManager.Instance.isPaused;
        ToggleDeathUI(GameManager.Instance.isPaused);
        if (backgroundManager != null)
        {
            if (backgroundManager.audioSourcePlayingCurrent)
            {
                if (GameManager.Instance.isPaused)
                {
                    print("Made it here");
                    backgroundManager.audioSource.Pause();
                }
            }
            else
            {
                if (GameManager.Instance.isPaused)
                {
                    backgroundManager.secondaryAudio.Pause();
                }
            }
        }
        Cursor.visible = GameManager.Instance.isPaused;
        Cursor.lockState = GameManager.Instance.isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Time.timeScale = GameManager.Instance.isPaused ? 0 : 1;
    }

    private void Respawn()
    {
        SetDeathState(false);
        PublicEvents.OnPlayerRespawn.Invoke();
    }

    private void goToMain()
    {
        SaveDataManager.Instance.OnApplicationQuit();
        SceneManager.LoadScene(mainMenuSceneName);
    }

    private void ToggleDeathUI(bool toggle)
    {
        deathGroup.alpha = toggle ? 1 : 0;
        deathGroup.interactable = toggle;
        deathGroup.blocksRaycasts = toggle;
    }

    public void SetDeathState(bool state)
    {
        GameManager.Instance.isPaused = state;
        ToggleDeathUI(state);

        Cursor.visible = state;
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        Time.timeScale = state ? 0 : 1;
    }
}
