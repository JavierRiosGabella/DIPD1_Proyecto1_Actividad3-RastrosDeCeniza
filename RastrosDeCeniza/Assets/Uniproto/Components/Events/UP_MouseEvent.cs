using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UP_MouseEvent : MonoBehaviour {

    public enum MouseButton
    {
        LeftClick,
        RightClick,
        MiddleClick
    }

    [SerializeField] MouseButton button = MouseButton.LeftClick;
    [SerializeField] float timeBetweenEvents = 0;

    [SerializeField] UP_NoArgsUnityEvent onMouseDown = new UP_NoArgsUnityEvent();
    [SerializeField] UP_NoArgsUnityEvent onMouse = new UP_NoArgsUnityEvent();
    [SerializeField] UP_NoArgsUnityEvent onMouseUp = new UP_NoArgsUnityEvent();

    float onMouseDownTime = 0;
    float onMouseUpTime = 0;
    float onMouseTime = 0;


    void Update()
    {
        onMouseDownTime = Mathf.Max(0, onMouseDownTime - Time.deltaTime);
        onMouseUpTime = Mathf.Max(0, onMouseUpTime - Time.deltaTime);
        
        if (onMouseDownTime == 0 && Input.GetMouseButtonDown((int)button))
        {
            onMouseDownTime = timeBetweenEvents;
            onMouseDown.Invoke();
        }
        if (onMouseUpTime == 0 && Input.GetMouseButtonUp((int)button))
        {
            onMouseUpTime = timeBetweenEvents;
            onMouseUp.Invoke();
        }        
                
    }

    private void FixedUpdate()
    {
        onMouseTime = Mathf.Max(0, onMouseTime - Time.deltaTime);
        if (onMouseTime == 0 && Input.GetMouseButton((int)button))
        {
            onMouseTime = timeBetweenEvents;
            onMouse.Invoke();
        }
    }

}
