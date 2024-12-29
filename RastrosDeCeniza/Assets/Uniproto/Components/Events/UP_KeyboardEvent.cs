using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class UP_KeyboardEvent : MonoBehaviour {

    [SerializeField] string key = "space";
    [SerializeField] float timeBetweenEvents = 0;
    
    [SerializeField] UP_NoArgsUnityEvent onKeyDown = new UP_NoArgsUnityEvent();
    [SerializeField] UP_NoArgsUnityEvent onKey = new UP_NoArgsUnityEvent();
    [SerializeField] UP_NoArgsUnityEvent onKeyUp = new UP_NoArgsUnityEvent();

    float onKeyDownCooldown = 0;
    float onKeyUpCooldown = 0;
    float onKeyCooldown = 0;

    void Update ()
    {
        onKeyDownCooldown = Mathf.Max(0, onKeyDownCooldown - Time.deltaTime);
        onKeyUpCooldown = Mathf.Max(0, onKeyUpCooldown - Time.deltaTime);
        if (onKeyDownCooldown == 0 && Input.GetKeyDown(key))
        {
            onKeyDownCooldown = timeBetweenEvents;
            onKeyDown.Invoke();
        }
        if (onKeyUpCooldown == 0 && Input.GetKeyUp(key))
        {
            onKeyUpCooldown = timeBetweenEvents;
            onKeyUp.Invoke();
        }        
	}

    private void FixedUpdate()
    {
        onKeyCooldown = Mathf.Max(0, onKeyCooldown - Time.deltaTime);
        if (onKeyCooldown == 0 && Input.GetKey(key))
        {
            onKeyCooldown = timeBetweenEvents;
            onKey.Invoke();
        }
    }

}
