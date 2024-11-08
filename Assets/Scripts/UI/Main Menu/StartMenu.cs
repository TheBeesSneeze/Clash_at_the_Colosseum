///
/// Initialized in MainMenuInitializer
/// -Toby
///

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace mainMenu
{
    public class StartMenu
    {
        public static CanvasGroup group;
        public MainMenuInitializer _mainMenu;

        private Button _playButton;
        private Button _tutorialButton;
        private Button _settingsButton;
        private Button _creditsButton;
        private Button _quitButton;
        private string _sceneToLoad;
        //private Button _confirmExitButton;

        public StartMenu(MainMenuInitializer menu, Button playButton, Button tutorialButton, Button creditsMenu, Button quitButton, CanvasGroup Group, string SceneToLoad)
        {
            _mainMenu = menu;
            _playButton = playButton;
            _tutorialButton = tutorialButton;
            _creditsButton = creditsMenu;
            _quitButton = quitButton;
            group = Group;
            _sceneToLoad = SceneToLoad;
            //_confirmExitButton = confirmExitButton;

            _playButton.onClick.AddListener(playClicked);
            _tutorialButton.onClick.AddListener(tutorialClicked);
            _creditsButton.onClick.AddListener(creditsClicked);
            _quitButton.onClick.AddListener(quitClicked);
        }

        public void playClicked()
        {
            //_mainMenu.animator.SetTrigger("Gun Select");
            SceneManager.LoadScene(_sceneToLoad);
            Debug.Log("play clicked");
            //_mainMenu.Start_toGunSelect();
        }
        public void tutorialClicked()
        {
            Debug.Log("Tutorial clicked");

            MainMenuInitializer.ToggleCanvasGroup(group, false);
            MainMenuInitializer.ToggleCanvasGroup(TutorialMenu.group, true);
        }
        public void settingsClicked()
        {
        }

        public void creditsClicked()
        {
            MainMenuInitializer.ToggleCanvasGroup(group, false);
            MainMenuInitializer.ToggleCanvasGroup(CreditsMenu.group, true);
        }
        public void quitClicked()
        {
            Debug.Log("Exiting Game...");
            Application.Quit();
        }
        public void confirmExitClicked()
        {
            Debug.Log("Exiting Game...");
            Application.Quit();
        }
    }
}
