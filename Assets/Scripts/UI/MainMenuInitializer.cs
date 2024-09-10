/*******************************************************************************
 * File Name :         MainMenuInitializer.cs
 * Author(s) :         Tyler, Toby
 * Creation Date :     9/3/2024
 *
 * Brief Description : Initializes different panels of the main menu.
 * I love overcomplicating things!!!!!!!!!!!
 *****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace mainMenu
{
    public class MainMenuInitializer : MonoBehaviour
    {
        [Header("Variables")]
        public string mainSceneName = "Gun Test";

        [Header("Start Menu")]
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _statsButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _creditsButton;
        [SerializeField] private Button _quitButton;
        //[SerializeField] private Button _confirmExitButton;

        [HideInInspector] public StartMenu startMenu;

        private void Awake()
        {
            startMenu = new StartMenu(this, _playButton, _statsButton, _settingsButton, _creditsButton, _quitButton);
        }

    }
}