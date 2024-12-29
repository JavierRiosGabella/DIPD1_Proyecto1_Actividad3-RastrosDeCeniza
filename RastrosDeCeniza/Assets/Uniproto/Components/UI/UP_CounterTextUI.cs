using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class UP_CounterTextUI : MonoBehaviour {

    
    [SerializeField] UP_Counter counter = null;
    [SerializeField] Text text = null;
    [SerializeField] string prefix = null;
    [SerializeField] string suffix = null;

    int lastValue;

    void Awake()
    {
        UpdateText();
    }

    void Update()
    {
        if(lastValue != counter.Counter)
        {
            UpdateText();
        }
    }

    void UpdateText()
    {
        lastValue = counter.Counter;
        text.text = prefix + counter.Counter + suffix;
    }


}
