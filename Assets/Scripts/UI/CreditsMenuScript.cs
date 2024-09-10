using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenuScript : MonoBehaviour
{
    public GameObject ParentMenu;
    public GameObject creditsButtonsContainer;
    public GameObject backButton;
    public GameObject programmersButton;
    public GameObject designersButton;
    public GameObject artistsButton;
    public GameObject otherAssetsButton;
    private void Start()
    {
        InputEvents.Instance.PauseStarted.AddListener(escPressed);
    }
    public void escPressed() {
        ParentMenu.SetActive(true);
        creditsButtonsContainer.SetActive(false);
    }
    public void programmersButtonClicked() {
        print("Programmers button clicked");
    }
    public void DesignersButtonClicked() {
        print("Designers button clicked");
    }
    public void artistsButtonClicked() {
        print("Artists button clicked");
    }
    public void otherAssetsButtonClicked() {
        print("Other assets button clicked");
    }
}
