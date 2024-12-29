using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UP_CounterBarUI : MonoBehaviour
{

    [SerializeField] UP_Counter counter = null;
    [SerializeField] Image barImage = null;
    [SerializeField] int minCounterValue = 0;
    [SerializeField] int maxCounterValue = 10;

    void Update()
    {
        float value = counter.Counter - minCounterValue;
        barImage.fillAmount = Mathf.Clamp01(value / maxCounterValue);
    }
}
