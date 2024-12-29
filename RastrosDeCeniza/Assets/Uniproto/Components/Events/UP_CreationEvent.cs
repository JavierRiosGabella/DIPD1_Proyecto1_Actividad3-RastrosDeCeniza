using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class UP_CreationEvent : MonoBehaviour {

    [SerializeField] UP_NoArgsUnityEvent onCreate = null;
   
    void Start () {
        onCreate.Invoke();
	}

    
}
