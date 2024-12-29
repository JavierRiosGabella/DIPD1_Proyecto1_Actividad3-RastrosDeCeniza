using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;


public class UP_CollisionEvent : MonoBehaviour {

    [SerializeField] bool filterByTag = false;    
    [TagSelector,SerializeField] string collisionTag = null;
    [SerializeField] bool sendMessage = false;
    [SerializeField] string message = null;
    [SerializeField] UP_NoArgsUnityEvent onCollisionEnter = new UP_NoArgsUnityEvent();
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(!filterByTag || collision.gameObject.CompareTag(collisionTag))
        {
            onCollisionEnter.Invoke();
            if (sendMessage) { SendMessage(collision.collider); }
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

    [UnityEditor.CustomEditor(typeof(UP_CollisionEvent))]
    public class CustomEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            UP_CollisionEvent componente = target as UP_CollisionEvent;

            serializedObject.Update();

            EditorGUILayout.LabelField("Filter", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("filterByTag"));
            if (componente.filterByTag) { EditorGUILayout.PropertyField(serializedObject.FindProperty("collisionTag")); }

            EditorGUILayout.LabelField("Messages", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("sendMessage"));
            if(componente.sendMessage) { EditorGUILayout.PropertyField(serializedObject.FindProperty("message")); }

            EditorGUILayout.LabelField("Events", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onCollisionEnter"));
            serializedObject.ApplyModifiedProperties();

        }
    }

#endif
}
