using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using System.Threading.Tasks;

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
        ToggleDeathUI(false);

        if (backgroundManager == null)
            backgroundManager = FindObjectOfType<BackgroundManager>();

        PublicEvents.OnPlayerDeath.AddListener(showScreen);
        respawnButton.onClick.AddListener(Respawn);
        mainMenuButton.onClick.AddListener(goToMain);

    }


    private void showScreen()
    {
        Music();
        GameManager.Instance.isPaused = !GameManager.Instance.isPaused;
        ToggleDeathUI(GameManager.Instance.isPaused);
        Cursor.visible = GameManager.Instance.isPaused;
        Cursor.lockState = GameManager.Instance.isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Time.timeScale = GameManager.Instance.isPaused ? 0 : 1;
    }

    private void Music()
    {
        if (backgroundManager != null)
        {
            if (backgroundManager.audioSourcePlayingCurrent)
            {
                backgroundManager.audioSource.Stop();
            }
            else
            {
                backgroundManager.secondaryAudio.Stop();
            }

        }
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
