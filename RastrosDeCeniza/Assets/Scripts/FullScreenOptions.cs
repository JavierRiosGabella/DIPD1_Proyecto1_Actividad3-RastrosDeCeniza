using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FullScreenOptions : MonoBehaviour
{
    private const string _keyResolution = "resolutionIndex";
    public Toggle toggle;
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    void Start()
    {
        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        } else
        {
            toggle.isOn = false;
        }

        ReturnResolutions();
    }

    public void SetFullScreen(bool isActive)
    {
        Screen.fullScreen = isActive;
    }


    /// <summary>
    /// Guarda las resoluciones que tenemos disponibles y las agrega al dropdown para hacerlas visible.
    /// </summary>
    public void ReturnResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int ActualResolution = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (Screen.fullScreen &&
                resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                ActualResolution = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = ActualResolution;
        resolutionDropdown.RefreshShownValue();

        resolutionDropdown.value = PlayerPrefs.GetInt(_keyResolution, 0);
    }

    public void SetResolution(int resolutionIndex)
    {
        PlayerPrefs.SetInt(_keyResolution, resolutionDropdown.value);

        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
