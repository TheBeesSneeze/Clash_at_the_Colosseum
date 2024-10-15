
///
/// Toby
/// TODO:
/// animate the select button
///

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Threading.Tasks;


public class UpgradeSelectUI : MonoBehaviour
{
    
    [SerializeField] private CanvasGroup group;
    [SerializeField] private BulletEffectButton upgradeButton1=new BulletEffectButton();
    [SerializeField] private BulletEffectButton upgradeButton2=new BulletEffectButton();
    [Space(5)]
    [SerializeField] private Button selectButton;
    [SerializeField] private Image selectButtonImage;
    [SerializeField] private Color notSelectedTint;

    private List<BulletEffect> bulletEffectPool;
    private GunController _gunController;
    private BulletEffect selectedEffect;
    private bool open = true;

    private void Start()
    {
        _gunController = GameObject.FindObjectOfType<GunController>();
        bulletEffectPool = new List<BulletEffect>(GameManager.Instance.BulletEffects);

        DisableMenu();

        upgradeButton1.button.onClick.AddListener(OnUpgradeButton1Clicked);
        upgradeButton2.button.onClick.AddListener(OnUpgradeButton2Clicked);
        selectButton.onClick.AddListener(OnSelectClick);
    }

    public async void OpenMenu(float delaySeconds = 2.5f)
    {
        if (open) return;
        open = true;
        await Task.Delay((int)(delaySeconds * 1000));

        GameManager.Instance.pausedForUI = true;
        EnableMenu();
        selectButton.interactable = false;
        Time.timeScale = 0f;


        Debug.Log("opening menu");
        SetUpgradeEffects();
    }

    private void EnableMenu()
    {
        group.alpha = 1;
        group.interactable = true;
        group.blocksRaycasts = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void DisableMenu()
    {
        group.alpha = 0;
        group.interactable = false;
        group.blocksRaycasts = false;
        selectButton.interactable = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameManager.Instance.pausedForUI = false;
        Time.timeScale = 1;
        open = false;
    }

    private void OnUpgradeButton1Clicked()
    {
        LoadUpgradeEffectOnClick(upgradeButton1.bulletEffect);

        ColorBlock cb1 = upgradeButton1.button.colors;
        cb1.normalColor = Color.white;
        cb1.highlightedColor = Color.white;
        cb1.selectedColor = Color.white;
        upgradeButton1.button.colors = cb1;

        ColorBlock cb2 = upgradeButton2.button.colors;
        cb2.normalColor = notSelectedTint;
        cb2.highlightedColor = notSelectedTint;
        cb2.selectedColor = notSelectedTint;
        upgradeButton2.button.colors = cb2;
    }

    private void OnUpgradeButton2Clicked()
    {
        LoadUpgradeEffectOnClick(upgradeButton2.bulletEffect);

        ColorBlock cb2 = upgradeButton2.button.colors;
        cb2.normalColor = Color.white;
        cb2.highlightedColor = Color.white;
        cb2.selectedColor = Color.white;
        upgradeButton2.button.colors = cb2;

        ColorBlock cb1 = upgradeButton1.button.colors;
        cb1.normalColor = notSelectedTint;
        cb1.highlightedColor = notSelectedTint;
        cb1.selectedColor = notSelectedTint;
        upgradeButton1.button.colors = cb1;
    }

    private void LoadUpgradeEffectOnClick(BulletEffect effect)
    {
        selectButton.interactable = true;
        selectButtonImage.color = effect.secondaryColor;
        selectedEffect = effect;
    }


    private void OnSelectClick()
    {
        _gunController.AddBulletEffect (selectedEffect);
        bulletEffectPool.Remove(selectedEffect);
        DisableMenu();
        StageManager.ChangeStage();
    }


    private void SetUpgradeEffects()
    {
        //fuck you
        int count = bulletEffectPool.Count;
        Assert.IsTrue(count >= 2);

        int index1;
        int index2;

        do //holy shit guys practical use of a do while loop
        {
            index1 = Random.Range(0, count);
            index2 = Random.Range(0, count);
        }
        while(index1 == index2);

        upgradeButton1.LoadBulletEffect(bulletEffectPool[index1]);
        upgradeButton2.LoadBulletEffect(bulletEffectPool[index2]);
    }
}
