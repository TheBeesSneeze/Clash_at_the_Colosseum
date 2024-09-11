using mainMenu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace mainMenu
{
    public class GunSelectMenu
    {
        private Button _leftButton;
        private Button _rightButton;
        private Button _startButton;

        private MainMenuInitializer _mainMenu;
        private string _sceneToLoad;

        public GunSelectMenu(MainMenuInitializer mainMenu, string sceneToLoad, Button leftButton, Button rightButton, Button startButton)
        {
            _leftButton = leftButton;
            _rightButton = rightButton;
            _startButton = startButton;

            _mainMenu = mainMenu;
            _sceneToLoad = sceneToLoad;

            _startButton.onClick.AddListener(StartButtonPressed);
        }

        public void StartButtonPressed()
        {
            SceneManager.LoadScene(_sceneToLoad);
        }
    }
}

