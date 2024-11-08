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
using TMPro;
using NaughtyAttributes;

namespace mainMenu
{
    public class MainMenuInitializer : MonoBehaviour
    {
        [Header("Variables")]
        [Scene]
        [SerializeField] public string mainSceneName;

        [Header("Groups")]
        [SerializeField] private CanvasGroup startGroup;
        [SerializeField] private CanvasGroup gunSelectGroup;

        [Header("Start Menu")]
        [SerializeField] private Button _playButton;
        //[SerializeField] private Button _tutorialButton;
        [SerializeField] private Button _tutorialButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _creditsButton;
        [SerializeField] private Button _quitButton;
        [SerializeField] private RectTransform startButtonsHolder;
        [SerializeField] private RectTransform titleImage;
        [SerializeField] private Button _confirmExitButton;

        [Header("Gun Select")]
        [SerializeField] private List<ShootingMode> GunTypes;

        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;
        [SerializeField] private CanvasGroup arrowsHolder;
        [SerializeField] private Button _startButton;
        [SerializeField] private RectTransform gunInfoGroup;
        [SerializeField] private RawImage headerImage;
        [SerializeField] private Image gunSprite;
        [SerializeField] private TMP_Text gunText;
        [SerializeField] private RectTransform tempGunInfoGroup;
        [SerializeField] private RawImage tempHeaderImage;
        [SerializeField] private Image tempGunSprite;
        [SerializeField] private TMP_Text tempGunText;
        [SerializeField] private Button gunSelectBackButton;

        [HideInInspector] public static StartMenu startMenu;
        [HideInInspector] public GunSelectMenu gunSelectMenu;
        [HideInInspector] public static TutorialMenu tutorialMenu;
        [HideInInspector] public CreditsMenu creditsMenu;
        [HideInInspector] public Animator animator;

        [Header("Tutorial")]
        [SerializeField] private CanvasGroup tutorialGroup;
        [SerializeField] private Button tutorialBackButton;

        [Header("Credits")]
        [SerializeField] private CanvasGroup creditsGroup;
        [SerializeField] private Button creditsBackButton;
        private void Start()
        {
            Time.timeScale = 1.0f;  
            animator = GetComponent<Animator>();
            animator.SetTrigger("Start Menu");
            startMenu = new StartMenu(this, _playButton, _tutorialButton, _settingsButton, _creditsButton, _quitButton, startGroup);
            gunSelectMenu = new GunSelectMenu(this, mainSceneName, GunTypes, 
                _leftButton, _rightButton, _startButton, 
                gunInfoGroup, headerImage, gunSprite, gunText,
                tempGunInfoGroup, tempHeaderImage, tempGunSprite, tempGunText, gunSelectBackButton, gunSelectGroup);
            creditsMenu = new CreditsMenu(creditsGroup, creditsBackButton);
            tutorialMenu = new TutorialMenu(tutorialGroup, tutorialBackButton);

            ToggleCanvasGroup(startGroup, true);
            ToggleCanvasGroup(gunSelectGroup, false);
            ToggleCanvasGroup(creditsGroup, false);
            ToggleCanvasGroup(tutorialGroup, false);
        }

        public static void ToggleCanvasGroup(CanvasGroup group, bool enabled)
        {
            group.alpha = enabled ? 1 : 0;
            group.interactable = enabled;
            group.blocksRaycasts = enabled;
        }

        /*
        #region hardcoded animations

        public void Start_toGunSelect()
        {
            StartCoroutine(Start_toGunSelectAnimation());
        }

        private IEnumerator Start_toGunSelectAnimation()
        {
            gunSelectGroup.interactable = true;
            gunSelectGroup.blocksRaycasts = true;
            startGroup.interactable = false;
            startGroup.blocksRaycasts = false;
            arrowsHolder.interactable = true;
            arrowsHolder.blocksRaycasts = true;
            //.alpha = 0;

            //gunSprite.transform.localScale = Vector3.one * 0.7f;
            //gunSprite.color = new Color(1, 1, 1, 0);

            // a) 0 -> 0.3 
            Color bg_start = background.color; //new Color(112 / 255, 72 / 255, 11 / 255);
            Color bg_end   = new Color(43 / 255,  26 / 255, 0  / 255);
            Vector2 btnHolder_start = startButtonsHolder.anchoredPosition;
            Vector2 btnHolder_end = new Vector2(-1920, 0);
            float titleWidth_start = titleImage.sizeDelta.x;
            float titleWidth_end = titleWidth_start / 2;

            // b) 0 -> 1.0 
            float startAlpha_start = startGroup.alpha;
            float startAlpha_end = 0;
            float scanlinesAlpha_start = 0;
            float scanlinesAlpha_end = scanlines.color.a;

            // c) 0.3 -> 1.3 
            Vector3 gunImage_start = Vector3.zero;
            Vector3 gunImage_end = gunSprite.rectTransform.localScale;
            float gunImageAlpha_start = 0;
            float gunImageAlpha_end = gunSprite.color.a;

            // d) 1.0 -> 2.0
            float arrowsAlpha_start = 0;
            float arrowsAlpha_end = arrowsHolder.alpha;

            float t = 0;
            float a, b, c, d = 0;

            while(t <= 2)
            {
                t += Time.deltaTime;
                Debug.Log(t);
                a = Mathf.Min(t, 0.3f) / 0.3f;
                b = Mathf.Min(t, 1);
                c = (t >= 0.3f) ? (Mathf.Clamp(t, 0.3f, 1.3f)-0.3f) : 0;
                d = (t >= 1) ? (t-1): 0;

                Debug.Log("a: " + a + " b: " + b + " c: " + c + " d: " + d);

                yield return null;
            }
        }

        #endregion
        */
    }
}