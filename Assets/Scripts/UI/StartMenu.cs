///
/// Initialized in MainMenuInitializer
/// -Toby
///

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace mainMenu
{
    public class StartMenu
    {
        public MainMenuInitializer _mainMenu;

        private Button _playButton;
        private Button _statsButton;
        private Button _settingsButton;
        private Button _creditsButton;
        private Button _quitButton;
        //private Button _confirmExitButton;

        public StartMenu(MainMenuInitializer menu, Button playButton, Button statsButton, Button settingMenu, Button creditsMenu, Button quitButton)
        {
            _mainMenu = menu;
            _playButton = playButton;
            _statsButton = statsButton;
            _settingsButton = settingMenu;
            _creditsButton = creditsMenu;
            _quitButton = quitButton;
            //_confirmExitButton = confirmExitButton;

            _playButton.onClick.AddListener(playClicked);
            _statsButton.onClick.AddListener(statsClicked);
            _settingsButton.onClick.AddListener(settingsClicked);
            _creditsButton.onClick.AddListener(creditsClicked);
            _quitButton.onClick.AddListener(quitClicked);
        }

        public void playClicked()
        {
            Debug.Log("play clicked");
        }
        public void statsClicked()
        {
            Debug.Log("Stats clicked");
        }
        public void settingsClicked()
        {
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
}
