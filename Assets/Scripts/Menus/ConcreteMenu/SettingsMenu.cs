using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
public class SettingsMenu : BaseMenu
{
    public AudioMixer audioMixer;

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
        {
            SetupSliderInformation(masterVolSlider, masterVolText, "MasterVol");
            OnSliderValueChanged(masterVolSlider.value, masterVolSlider, masterVolText, "MasterVol");
        }
        if (musicVolSlider)
        {
            SetupSliderInformation(musicVolSlider, musicVolText, "MusicVol");
            OnSliderValueChanged(musicVolSlider.value, musicVolSlider, musicVolText, "MusicVol");
        }

        if (sfxVolSlider)
        {
            SetupSliderInformation(sfxVolSlider, sfxVolText, "SFXVol");
            OnSliderValueChanged(sfxVolSlider.value, sfxVolSlider, sfxVolText, "SFXVol");
        }
    }
    private void SetupSliderInformation(Slider slider, TMP_Text text, string parameterName)
    {
        slider.onValueChanged.AddListener((value) => OnSliderValueChanged(value, slider, text, parameterName));
    }

    private void OnSliderValueChanged(float value, Slider slider, TMP_Text text, string parameterName)
    {
        if (value == 0)
        {
            value = -80;
            text.text = $"0%";
        }
        else
        {
            value = Mathf.Log10(value) * 20;
            text.text = $"{Mathf.RoundToInt(slider.value * 100)}%";
        }

        audioMixer.SetFloat(parameterName, value);
    }

}
