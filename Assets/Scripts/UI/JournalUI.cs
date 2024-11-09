// Code made by Brooke Boster

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.ComponentModel.Design.Serialization;

public class JournalUI : MonoBehaviour
{
    // This is a List that contains all of the pages. It can be changed in inspector.
    [Tooltip("This list should contain the pages in the canvas in hierarchy! The number you put is the actual number of pages unlike pageCap variable")]
    public List<GameObject> pages = new List<GameObject>();


    // This page number will change and help navigate around the list to flip pages.
    // It is set to 0 currently because the list should start at 0 and make it's way up to 7, so we have total 8 pages
    [ShowNonSerializedField]
    private int pageNumber = 0;

    // This variable decides how many pages their can be total, this avoid an out of range error!
    // REMINDER THIS WILL BE ONE LESS THAN THE ACTUAL PAGE COUNT SINCE WE START WITH 0 NOT 1
    //[Tooltip("Pages start at 0 and count up from there. For example if you have 8 pages total you will enter 7 here")]
    private int pageCap;

    // To control the buttons and canvas, strictly for me bc I want it to look pretty
    [Tooltip("DO NOT TOUCH")]
    [SerializeField] private GameObject backButton;
    [Tooltip("DO NOT TOUCH")]
    [SerializeField] private GameObject forwardButton;
    [Tooltip("DO NOT TOUCH")]
    [SerializeField] private GameObject exitButton;
    //[SerializeField] private GameObject returnToJournalButton;
    [Tooltip("DO NOT TOUCH, if this Canvas is set to false it MUST be set to true somewhere else when inititating the Journal, this means that " +
        "this variable must be referenced in pause menu code!")]
    [SerializeField] private GameObject Journal;

    private CanvasGroup group;


    private void Start()
    {
        pageCap = pages.Count - 1;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Journal.SetActive(true);
        group = Journal.transform.parent.GetComponent<CanvasGroup>();

        group.alpha = 1.0f;
        group.interactable = true;
        group.blocksRaycasts = true;

        FlipPageAny();
    }

    /// <summary>
    /// This function will flip us back a page
    /// </summary>
    public void FlipPageBack()
    {
        pages[pageNumber].SetActive(false);
        pageNumber = Mathf.Max(0, pageNumber-1);
        FlipPageAny();
    }

    /// <summary>
    /// This function will flip us forward a page
    /// </summary>
    public void FlipPageForward()
    {
        // Here we will add 1 to the pageNumber if we are not at the last page, this will bring us forward one page in the Journal.
        pages[pageNumber].SetActive(false);
        pageNumber = Mathf.Min(pageCap, pageNumber+1);
        FlipPageAny();
    }

    public void FlipPageAny()
    {
        pages[pageNumber].SetActive(true);

        backButton.SetActive(pageNumber != 0);
        forwardButton.SetActive(pageNumber != pageCap);
    }

    /// <summary>
    /// Will turn off the canvas...turn it back on in main menu code!
    /// </summary>
    public void ExitCanvas()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //returnToJournalButton.SetActive(true);

        group.alpha = 0f;
        group.interactable = false;
        group.blocksRaycasts = false;
    }

    // This will pull the journal back up starting at page one.
    // This function really will only be necessary in the first room...not the colosseum. 
    public void PullBackUpJournal()
    {
        //returnToJournalButton.SetActive(false);
        pageNumber = 0;
        backButton.SetActive(true);
        forwardButton.SetActive(true);
        exitButton.SetActive(true);

        group.alpha = 1.0f;
        group.interactable = true;
        group.blocksRaycasts = true;
    }
}
