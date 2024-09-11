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

        [Header("Groups")]
        [SerializeField] private CanvasGroup startGroup;
        [SerializeField] private CanvasGroup gunSelectGroup;

        [Header("Start Menu")]
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _statsButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _creditsButton;
        [SerializeField] private Button _quitButton;
        //[SerializeField] private Button _confirmExitButton;

        [Header("Gun Select")]
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;
        [SerializeField] private Button _startButton;

        [HideInInspector] public StartMenu startMenu;
        [HideInInspector] public GunSelectMenu gunSelectMenu;
        [HideInInspector] public Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            startMenu = new StartMenu(this, _playButton, _statsButton, _settingsButton, _creditsButton, _quitButton);
            gunSelectMenu = new GunSelectMenu(this, mainSceneName, _leftButton, _rightButton, _startButton);

            ToggleCanvasGroup(startGroup, true);
            ToggleCanvasGroup(gunSelectGroup, false);
        }

        public static void ToggleCanvasGroup(CanvasGroup group, bool enabled)
        {
            group.alpha = enabled ? 1 : 0;
            group.interactable = enabled;
            group.blocksRaycasts = enabled;

        }
    }
}