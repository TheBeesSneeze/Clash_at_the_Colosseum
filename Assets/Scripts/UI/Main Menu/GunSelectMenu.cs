using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEditor;

namespace UI.MainMenu
{
    public class GunSelectMenu
    {
        public static CanvasGroup group;

        private float animateTime = 0.2f;
        private float screenWidth = 1920;

        private Button _leftButton;
        private Button _rightButton;
        private Button _startButton;

        private RectTransform _gunInfoGroup;
        private RawImage _headerImage;
        private Image _gunSprite;
        private TMP_Text _gunText;

        private RectTransform _tempGunInfoGroup;
        private RawImage _tempHeaderImage;
        private Image _tempGunSprite;
        private TMP_Text _tempGunText;

        private MainMenuInitializer _mainMenu;
        private string _sceneToLoad;
        private List<ShootingMode> _gunTypes;

        private Button _backButton;

        private int currentGunIndex;

        public GunSelectMenu(MainMenuInitializer mainMenu, string sceneToLoad, List<ShootingMode> gunTypes,
            Button leftButton, Button rightButton, Button startButton, 
            RectTransform gunInfoGroup, RawImage headerImage, Image gunSprite, TMP_Text gunText,
            RectTransform tempGunInfoGroup, RawImage tempHeaderImage, Image tempGunSprite, TMP_Text tempGunText, 
            Button gunSelectBackButton, CanvasGroup Group)
        {
            _leftButton = leftButton;
            _rightButton = rightButton;
            _startButton = startButton;

            _gunInfoGroup = gunInfoGroup;
            _headerImage = headerImage;
            _gunSprite = gunSprite;
            _gunText = gunText;

            _tempHeaderImage = tempHeaderImage;
            _tempGunInfoGroup = tempGunInfoGroup;
            _tempGunSprite = tempGunSprite;
            _tempGunText = tempGunText;

            _mainMenu = mainMenu;
            _sceneToLoad = sceneToLoad;
            _gunTypes = gunTypes;

            _backButton = gunSelectBackButton;

            group = Group;

            LoadGun(_gunTypes[currentGunIndex]);

            _startButton.onClick.AddListener(StartButtonPressed);
            _leftButton.onClick.AddListener(LeftButtonPressed);
            _rightButton.onClick.AddListener(RightButtonPressed);
            _backButton.onClick.AddListener(BackButtonPressed);
        }

        public void StartButtonPressed()
        {
            SaveData.ResetData();   
            /*
            SaveData.SelectedGun = _gunTypes[currentGunIndex];
            SaveData.bulletEffectPool = new List<BulletEffect>();
            SaveData.gotBulletEffects = new List<BulletEffect>();
            SaveData.CurrentStageIndex = 0;
            */

            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            SceneManager.LoadScene(_sceneToLoad);
        }

        public void LeftButtonPressed()
        {

            
            int tempIndex = currentGunIndex;
            currentGunIndex = ((currentGunIndex - 1) + _gunTypes.Count) % _gunTypes.Count;
            animateLeft(_gunTypes[tempIndex], _gunTypes[currentGunIndex]);

            Debug.Log(_gunInfoGroup.rect);
            
        }

        public void RightButtonPressed()
        {
            int tempIndex = currentGunIndex;
            currentGunIndex = (currentGunIndex+1) % _gunTypes.Count;
            animateRight(_gunTypes[tempIndex], _gunTypes[currentGunIndex]);

            Debug.Log(_gunInfoGroup.rect);
        }

        private async void animateLeft(ShootingMode start, ShootingMode end)
        {
            float startTime = Time.time;
            float t = 0;

            _tempGunInfoGroup.anchoredPosition = new Vector2(-screenWidth, _tempGunInfoGroup.anchoredPosition.y);
            LoadTemp(end);


            while (t<1)
            {
                t = (Time.time - startTime) / animateTime;
                t = Mathf.Min(1, t);

                Color color = Color.Lerp(start.SelectHeaderColor, end.SelectHeaderColor,t);
                _headerImage.color = color;
                _tempHeaderImage.color = color;
                float x = screenWidth * t;
                _gunInfoGroup.anchoredPosition = new Vector2(x, _gunInfoGroup.anchoredPosition.y);
                _tempGunInfoGroup.anchoredPosition = new Vector2(x - screenWidth, _tempGunInfoGroup.anchoredPosition.y);
                await Task.Yield();
            }
            LoadGun(end);
            _gunInfoGroup.anchoredPosition = new Vector2(0, _gunInfoGroup.anchoredPosition.y);
            _tempGunInfoGroup.anchoredPosition = new Vector2(-screenWidth, _tempGunInfoGroup.anchoredPosition.y);
            
        }

        /// <summary>
        /// TODO: animateleft and animateright can totally be 1 function but i am lazy rn
        /// </summary>
        private async void animateRight(ShootingMode start, ShootingMode end)
        {
            float startTime = Time.time;
            float t = 0;

            _tempGunInfoGroup.anchoredPosition = new Vector2(screenWidth, _tempGunInfoGroup.anchoredPosition.y);
            LoadTemp(end);


            while (t < 1)
            {
                t = (Time.time - startTime) / animateTime;
                t = Mathf.Min(1, t);

                Color color = Color.Lerp(start.SelectHeaderColor, end.SelectHeaderColor, t);
                _headerImage.color = color;
                _tempHeaderImage.color = color;
                float x = screenWidth * -t;
                _gunInfoGroup.anchoredPosition = new Vector2(x, _gunInfoGroup.anchoredPosition.y);
                _tempGunInfoGroup.anchoredPosition = new Vector2(x + screenWidth, _tempGunInfoGroup.anchoredPosition.y);
                await Task.Yield();
            }
            LoadGun(end);
            _gunInfoGroup.anchoredPosition = new Vector2(0, _gunInfoGroup.anchoredPosition.y);
            _tempGunInfoGroup.anchoredPosition = new Vector2(screenWidth, _tempGunInfoGroup.anchoredPosition.y);

        }


        public void LoadGun(ShootingMode gun)
        {
            _gunText.text = gun.GunName.ToString();
            _gunSprite.sprite = gun.MenuSprite;
            _headerImage.color = gun.SelectHeaderColor;
        }

        public void LoadTemp(ShootingMode gun)
        {
            _tempGunText.text = gun.GunName.ToString();
            _tempGunSprite.sprite = gun.MenuSprite;
            _tempHeaderImage.color = gun.SelectHeaderColor;
        }

        public void BackButtonPressed()
        {
            Debug.Log("Gun Select -> Start Menu");
            _mainMenu.animator.SetTrigger("Start Menu");
            //MainMenuInitializer.ToggleCanvasGroup(group, false);
            //MainMenuInitializer.ToggleCanvasGroup(StartMenu.group, true);
        }
    }
}

