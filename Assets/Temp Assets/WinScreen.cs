using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NaughtyAttributes;

public class WinScreen : MonoBehaviour
{
    [Scene]
    [SerializeField] int mainMenuScene;
    [Scene]
    [SerializeField] int replayScene;
    [SerializeField] Button menuButton;
    [SerializeField] Button replayButton;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        menuButton.onClick.AddListener(ReturnToMainMenu);
        replayButton.onClick.AddListener(ReplayClicked);
    }
    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    private void ReplayClicked()
    {
        SceneManager.LoadScene(replayScene);
    }
}
