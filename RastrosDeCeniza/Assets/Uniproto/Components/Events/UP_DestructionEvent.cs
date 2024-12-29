using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class UP_DestructionEvent : MonoBehaviour {

    [SerializeField] UP_NoArgsUnityEvent onDestroy = null;

    bool isQuittingGame;

    void OnDestroy()
    {
        if(!isQuittingGame)
        {
            onDestroy.Invoke();
        }        
    }
    
    void OnApplicationQuit()
    {
        isQuittingGame = true;
    }
}
