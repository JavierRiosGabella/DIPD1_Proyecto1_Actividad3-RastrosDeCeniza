using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class UP_MessageEvent : MonoBehaviour {

    [SerializeField] string message = null;
    [SerializeField] UP_NoArgsUnityEvent onMessageReceived = null;    

	internal void ThrowMessageEvent(string mensaje)
    {
        if(this.isActiveAndEnabled && this.message == mensaje)
        {
            onMessageReceived.Invoke();
        }
    }
    
}
