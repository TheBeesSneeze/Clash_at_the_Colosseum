using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{


    public class TutorialMenu
    {
        public static CanvasGroup group;
        private Button backButton;
        public TutorialMenu(CanvasGroup Group, Button backButton)
        {
            group = Group;
            this.backButton = backButton;

            backButton.onClick.AddListener(OnBackButtonPressed);
        }

        void OnBackButtonPressed()
        {
            MainMenuInitializer.ToggleCanvasGroup(StartMenu.group, true);
            MainMenuInitializer.ToggleCanvasGroup(group, false);
        }
    }
}