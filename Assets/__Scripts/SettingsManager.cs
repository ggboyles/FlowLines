using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public Slider volumeSlider;
    public Toggle sfxToggle; // disabled from Settings view for now

    private const string VolumeKey = "volume";
    private const string SFXKey = "sfx_enabled";

    void Start()
    {
        // loads saved preferences
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
        bool sfxEnabled = PlayerPrefs.GetInt(SFXKey, 1) == 1;

        AudioListener.volume = savedVolume;
        volumeSlider.value = savedVolume;
        sfxToggle.isOn = sfxEnabled;

        volumeSlider.onValueChanged.AddListener(SetVolume);
        sfxToggle.onValueChanged.AddListener(SetSFX);
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat(VolumeKey, value);
    }

    public void SetSFX(bool enabled)
    {
        PlayerPrefs.SetInt(SFXKey, enabled ? 1 : 0);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public static bool IsSFXEnabled()
    {
        return PlayerPrefs.GetInt(SFXKey, 1) == 1;
    }
}