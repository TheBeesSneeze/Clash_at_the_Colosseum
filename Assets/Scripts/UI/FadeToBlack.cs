using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeToBlack : MonoBehaviour
{
    public Image FadeImage;
    private bool fadeStart = false;
    public float FadeTime;
    private float timePassed;

    void Start()
    {

        PublicEvents.HydraDeath.AddListener(Fading);
    }

    void Update()
    {
        if (fadeStart)
            FadingToBlack();

        
    }

    public void Fading()
    {
        fadeStart = true;
    }

    public void FadingToBlack()
    {
        if (fadeStart == true)
        {
            timePassed += Time.deltaTime;
            FadeImage.color = new Color(FadeImage.color.r, FadeImage.color.g, FadeImage.color.b, timePassed / FadeTime);
            if (timePassed >= FadeTime)
            {
                Debug.LogWarning("replace this");
                SceneManager.LoadScene("WinScreen");
            }
            

        }
    }
}
