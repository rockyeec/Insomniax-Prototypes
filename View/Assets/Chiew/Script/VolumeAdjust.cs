
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class VolumeAdjust : MonoBehaviour
{
    public Slider BGMSlider;
    public Slider SFXSlider;

    // Update is called once per frame
    private void Start()
    {
        BGMSlider.value = AudioManager.instance.BGM.First().volume;
        SFXSlider.value = AudioManager.instance.SFX.First().volume;
    }

    public void SetBGMVolume(float amount)
    {
        BGMSlider.value = amount;
        AudioManager.instance.AdjustTypeVolume(BGMSlider.value, "BGM");
    }

    public void SetSFXVolume(float amount)
    {
        SFXSlider.value = amount;
        AudioManager.instance.AdjustTypeVolume(SFXSlider.value, "SFX");
    }
}
