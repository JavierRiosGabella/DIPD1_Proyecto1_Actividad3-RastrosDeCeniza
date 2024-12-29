using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class UP_TriggerEvent : MonoBehaviour
{
    [SerializeField] bool filterByTag = false;
    [TagSelector, SerializeField] string filterTag = null;
    [SerializeField] bool sendMessage = false;
    [SerializeField] string message = null;
    [SerializeField] float timeBetweenStayEvents = 0;
    [SerializeField] UP_NoArgsUnityEvent onTriggerEnter = new UP_NoArgsUnityEvent();
    [SerializeField] UP_NoArgsUnityEvent onTriggerStay = new UP_NoArgsUnityEvent();
    [SerializeField] UP_NoArgsUnityEvent onTriggerExit = new UP_NoArgsUnityEvent();

    float stayWaitTime = 0;


    private void FixedUpdate()
    {
        stayWaitTime = Mathf.Max(0, stayWaitTime - Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!filterByTag || other.gameObject.CompareTag(filterTag))
        {
            if (onTriggerEnter != null) { onTriggerEnter.Invoke(); }
            if (sendMessage) { SendMessage(other); }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (stayWaitTime == 0 && (!filterByTag || other.gameObject.CompareTag(filterTag)))
        {
            stayWaitTime = timeBetweenStayEvents;
            if (onTriggerStay != null) { onTriggerStay.Invoke(); }
            if (sendMessage) { SendMessage(other); }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!filterByTag || other.gameObject.CompareTag(filterTag))
        {
            if (onTriggerExit != null) { onTriggerExit.Invoke(); }
            if (sendMessage) { SendMessage(other); }
        }
    }

    void SendMessage(Collider2D collider)
    {
        Component target = collider.attachedRigidbody;
        if (target == null) { target = collider; }

        UP_MessageEvent[] messageEvents = target.GetComponentsInChildren<UP_MessageEvent>();
        for (int i = 0; i < messageEvents.Length; ++i)
        {
            messageEvents[i].ThrowMessageEvent(message);
        }
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(UP_TriggerEvent))]
    public class CustomEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            UP_TriggerEvent component = target as UP_TriggerEvent;

            serializedObject.Update();

            EditorGUILayout.LabelField("Filter", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("filterByTag"));
            if (component.filterByTag) { EditorGUILayout.PropertyField(serializedObject.FindProperty("filterTag")); }

            EditorGUILayout.LabelField("Messages", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("sendMessage"));
            if (component.sendMessage) { EditorGUILayout.PropertyField(serializedObject.FindProperty("message")); }

            EditorGUILayout.LabelField("Events", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("timeBetweenStayEvents"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onTriggerEnter"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onTriggerStay"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onTriggerExit"));
            serializedObject.ApplyModifiedProperties();

        }
    }

#endif

}
