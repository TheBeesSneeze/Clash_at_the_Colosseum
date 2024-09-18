using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSelectUI : MonoBehaviour
{
    [SerializeField] private Button selectButton;
    [SerializeField] private CanvasGroup group;

    private void Start()
    {
        selectButton.onClick.AddListener(OnSelectClick);
    }

    public void EnableMenu()
    {
        group.alpha = 1;
        group.interactable = true;
        group.blocksRaycasts = true;
    }

    public void DisableMenu()
    {
        group.alpha = 0;
        group.interactable = false;
        group.blocksRaycasts = false;
    }

    public void OnSelectClick()
    {
        DisableMenu();
    }
}
