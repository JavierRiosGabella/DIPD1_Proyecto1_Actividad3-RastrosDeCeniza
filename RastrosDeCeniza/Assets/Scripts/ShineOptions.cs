using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShineOptions : MonoBehaviour
{
    private const string _key = "brillo";
    public Slider slider;
    public Image panelBrillo;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat(_key, 0.5f);
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, slider.value);
    }


    public void SetShineSlider(float value)
    {
        slider.value = PlayerPrefs.GetFloat(_key, value);
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, slider.value);
    }
}
