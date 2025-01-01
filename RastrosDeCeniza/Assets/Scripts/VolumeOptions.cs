using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeOptions : MonoBehaviour
{
    private const string _key = "volumenAudio";
    public Slider slider;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat(_key, 0.5f);
        AudioListener.volume = slider.value;
    }

    public void SetSlider(float value)
    {
        PlayerPrefs.SetFloat(_key, value);
        AudioListener.volume = slider.value;
    }
}
