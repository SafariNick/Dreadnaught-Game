using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
public class SettingsMenu : BaseMenu
{
    public AudioMixer AudioMixer;

    public Button backButton;
    public Button creditsButton;

    public TMP_Text masterVolText;
    public Slider masterVolSlider;

    public TMP_Text musicVolText;
    public Slider musicVolSlider;

    public TMP_Text sfxVolText;
    public Slider sfxVolSlider;

    public override void Init(MenuController currentContext)
    {
        base.Init(currentContext);
        state = MenuStates.Settings;

        if (backButton) backButton.onClick.AddListener(JumpBack);
        if (creditsButton) creditsButton.onClick.AddListener(() => JumpTo(MenuStates.Credits));

        if (masterVolSlider)
            SetupSliderInformation(masterVolSlider, masterVolText, "MasterVolume");
        if (musicVolSlider)
            SetupSliderInformation(musicVolSlider, musicVolText, "MusicVolume");
        if (sfxVolSlider)
            SetupSliderInformation(sfxVolSlider, sfxVolText, "SFXVolume");
    }
    private void SetupSliderInformation(Slider slider, TMP_Text text, string parameterName)
    {
        //slider.onValueChanged.AddListener((value) => OnSliderValueChanged(value, slider, text, parameterName));
    }
    private void OnSliderValueChanged(Slider slider, TMP_Text text, string parameterName)
    {
        //if ( value == 0)
        {
            
        }

    }
    
}
