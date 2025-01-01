using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ImageQualityOptions : MonoBehaviour
{
    private const string _key = "quality";
    public TMP_Dropdown dropdown;
    public int quality;

    void Start()
    {
        quality = PlayerPrefs.GetInt(_key, 3);
        dropdown.value = quality;
        SetQuality();
    }

    public void SetQuality()
    {
        QualitySettings.SetQualityLevel(dropdown.value);
        PlayerPrefs.SetInt(_key, dropdown.value);
        quality = dropdown.value;
    }
}
