using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeAdjust : MonoBehaviour
{
    public Slider BGMSlider;
    public Slider SFXSlider;

    //float BGMValue = 0.5f, SFXValue = 0.5f;
    // Update is called once per frame
    void Update()
    {
        AudioManager.instance.AdjustTypeVolume(BGMSlider.value, "BGM");
        AudioManager.instance.AdjustTypeVolume(SFXSlider.value, "SFX");
    }

    public void SetBGMVolume(float amount)
    {
        BGMSlider.value = amount;
        //BGMValue = amount;
    }

    public void SetSFXVolume(float amount)
    {
        SFXSlider.value = amount;
    }
}
