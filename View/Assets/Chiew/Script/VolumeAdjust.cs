using UnityEngine;
using UnityEngine.UI;

public class VolumeAdjust : MonoBehaviour
{
    public Slider BGMSlider;
    public Slider SFXSlider;

    // Update is called once per frame
    private void Start()
    {
        if(!PlayerPrefs.HasKey("bgmVolume"))
        {
            PlayerPrefs.SetFloat("bgmVolume", 0.5f);
            PlayerPrefs.SetFloat("sfxVolume", 0.5f);
        }
        SetBGMVolume(PlayerPrefs.GetFloat("bgmVolume"));
        SetSFXVolume(PlayerPrefs.GetFloat("sfxVolume"));
        BGMSlider.value = PlayerPrefs.GetFloat("bgmVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }

    public void SetBGMVolume(float amount)
    {
        PlayerPrefs.SetFloat("bgmVolume", amount);
        AudioManager.instance.AdjustTypeVolume(amount, "BGM");
    }

    public void SetSFXVolume(float amount)
    {
        PlayerPrefs.SetFloat("sfxVolume", amount);
        AudioManager.instance.AdjustTypeVolume(amount, "SFX");
    }
}
