///
/// Toby
///


using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BulletEffectButton
{
    [SerializeField] private Button _button;
    [SerializeField] private Image headerImage;
    [SerializeField] private TMP_Text upgradeText;
    [SerializeField] private Image bodyImage;
    [SerializeField] private Image effectIcon;

    private BulletEffect _bulletEffect;

    public Button button { get { return _button; } }

    public BulletEffect bulletEffect { get { return _bulletEffect; } }

    public void LoadBulletEffect(BulletEffect bulletEffect)
    {
        _bulletEffect = bulletEffect;
        Debug.Log("Loading " + bulletEffect.UpgradeName);

        headerImage.color = bulletEffect.secondaryColor;
        upgradeText.text = bulletEffect.UpgradeName;
        bodyImage.color = bulletEffect.bodyColor;
        effectIcon.sprite = bulletEffect.UpgradeIcon;
    }
}