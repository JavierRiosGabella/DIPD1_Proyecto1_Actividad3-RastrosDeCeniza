using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UP_UpdateEvent : MonoBehaviour {

    [SerializeField] UP_NoArgsUnityEvent onUpdate = null;

    void FixedUpdate()
    {   
        onUpdate.Invoke();   
    }
}
