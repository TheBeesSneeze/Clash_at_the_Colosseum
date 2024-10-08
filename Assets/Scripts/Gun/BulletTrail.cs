using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    [SerializeField] TrailRenderer trail;
    [SerializeField] float transitionSeconds = 1;
    private static GunController _gunController;
    private List<BulletEffect> effects { get { return _gunController.bulletEffects; } }

    private float timeElapsed = 0;
    private float t;
    private int index1=0;
    private int index2 = 1;
    private int index3 = 2;

    private void Awake()
    {
        if(_gunController == null)
            _gunController = GameObject.FindObjectOfType<GunController>();

        if(trail == null ) trail = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        if (!gameObject.activeSelf)
            return;

        timeElapsed += Time.deltaTime;

        if (timeElapsed >= transitionSeconds)
        {
            timeElapsed = 0;
            index1 = (index1 + 1) % effects.Count;
            index2 = (index1 + 1) % effects.Count;
            index3 = (index2 + 1) % effects.Count;
        }

        switch (effects.Count)
        {
            case 0:
                break;
            case 1:
                SetOneColor();
                break;
            case 2:
                SetTwoColors();
                break;
            default:
                SetThreeColors();
                break;
        }
    }

    private void SetOneColor()
    {
        GradientColorKey[] colors = new GradientColorKey[1];
        colors[0] = new GradientColorKey(effects[0].TrailColor, 0.0f);

        GradientAlphaKey[] alphas = new GradientAlphaKey[1];
        alphas[0] = new GradientAlphaKey(1, 0.0f);

        Gradient gradient = new Gradient();
        gradient.SetKeys(colors, alphas);
        trail.colorGradient = gradient;
    }

    private void SetTwoColors()
    { 
        t = timeElapsed / transitionSeconds;
        Color color1 = Color.Lerp(effects[index1].TrailColor, effects[index2].TrailColor, t);
        Color color2 = Color.Lerp(effects[index2].TrailColor, effects[index1].TrailColor, t);

        GradientColorKey[] colors = new GradientColorKey[2];
        colors[0] = new GradientColorKey(color1, 0.0f);
        colors[1] = new GradientColorKey(color2, 1.0f);

        GradientAlphaKey[] alphas = new GradientAlphaKey[1];
        alphas[0] = new GradientAlphaKey(1, 1.0f);

        Gradient gradient = new Gradient();
        gradient.SetKeys(colors, alphas);
        trail.colorGradient = gradient;
    }

    private void SetThreeColors()
    {
        t = timeElapsed / transitionSeconds;

        Color color1 = Color.Lerp(effects[index1].TrailColor, effects[index2].TrailColor, t);
        Color color2 = Color.Lerp(effects[index2].TrailColor, effects[index3].TrailColor, t);
        Color color3 = Color.Lerp(effects[index3].TrailColor, effects[index1].TrailColor, t);

        GradientColorKey[] colors = new GradientColorKey[3];
        colors[0] = new GradientColorKey(color1, 0.0f);
        colors[1] = new GradientColorKey(color2, 0.5f);
        colors[2] = new GradientColorKey(color3, 1.0f);

        GradientAlphaKey[] alphas = new GradientAlphaKey[1];
        alphas[0] = new GradientAlphaKey(1, 1.0f);

        Gradient gradient = new Gradient();
        gradient.SetKeys(colors, alphas);
        trail.colorGradient = gradient;
    }
}
