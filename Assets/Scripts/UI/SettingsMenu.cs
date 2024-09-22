///
/// - Tyler
///

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    //moved to pause menu, for now
    /*
    public GameObject ParentMenu;
    public GameObject buttonContainer;
    public GameObject backButton;
    public GameObject volumeSlider;
    public GameObject sensitivitySlider;
    public GameObject inputSwitch;
    public GameObject inputSwitchText;
    public void Start()
    {
        InputEvents.Instance.PauseStarted.AddListener(escPressed);
        Slider slider = volumeSlider.GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat("volume", 1);
        slider = sensitivitySlider.GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat("sensitivity", 1);
    }
    public void escPressed()
    {
        if (buttonContainer.activeSelf) {
            ParentMenu.SetActive(true);
            buttonContainer.SetActive(false);
        }
    }
    public void inputSwitchClicked()
    {
        Text inputbuttonText = inputSwitchText.GetComponent<Text>();
        if (inputbuttonText.text == "Input: Keyboard"){
            inputbuttonText.text = "Input: Controller";
        }else{
            inputbuttonText.text = "Input: Keyboard";
        }
    }
    public void volumeChanged()
    {
        Slider slider = volumeSlider.GetComponent<Slider>();
        PlayerPrefs.SetFloat("volume", slider.value);
        float sliderValue = slider.value;
        PublicEvents.OnSensitivitySliderChanged.Invoke();
    }
    public void sensitivityChanged()
    {
        Slider slider = sensitivitySlider.GetComponent<Slider>();
        PlayerPrefs.SetFloat("sensitivity", slider.value);
        float sliderValue = slider.value;
        PublicEvents.OnSensitivitySliderChanged.Invoke();
    }
    */
}
