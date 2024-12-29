using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UP_Messenger : MonoBehaviour {

    public enum ReceiverType
    {
        ByReference,
        ByName,
        ByTag
    }

    [SerializeField] ReceiverType receiverType = ReceiverType.ByTag;
    [SerializeField] GameObject receiver = null;
    [SerializeField] string receiverName = null;
    [TagSelector,SerializeField] string receiverTag = null;
    [SerializeField] string message = null;
    [SerializeField] string[] messages = null;
    [SerializeField] bool randomizeMessage = false;


    private void Start()
    {
        if(receiverType == ReceiverType.ByName)
        {
            receiver = GameObject.Find(receiverName);
        }
    }

    internal void SendMessage()
    {
        UP_SendMessage();
    }

    public void UP_SendMessage()
    {
        string messageToSend;
        if (randomizeMessage) { messageToSend = messages.Length > 0 ? messages[Random.Range(0, messages.Length)] : null; }
        else { messageToSend = message; }

        switch(receiverType)
        {
            case ReceiverType.ByReference:
                if (receiver != null) { SendMessage(receiver, messageToSend); }
                break;
            case ReceiverType.ByName:
                if(receiver == null) { receiver = GameObject.Find(receiverName); }
                if (receiver != null) { SendMessage(receiver, messageToSend); }
                break;
            case ReceiverType.ByTag:
                GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(receiverTag);
                foreach (var o in taggedObjects) { SendMessage(o, messageToSend); }
                break;
        }        
    }

    void SendMessage(GameObject o, string message)
    {
        UP_MessageEvent[] receivers = o.GetComponentsInChildren<UP_MessageEvent>();
        if(receivers.Length == 0)
        {
            Debug.LogWarning("No receiver for message: " + message, this);
        }
        for (int i = 0; i < receivers.Length; i++)
        {
            receivers[i].ThrowMessageEvent(message);
        }
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(UP_Messenger))]
    public class CustomEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            UP_Messenger sender = target as UP_Messenger;

            serializedObject.Update();

            EditorGUILayout.LabelField("Receiver", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("receiverType"));

            if (sender.receiverType == ReceiverType.ByReference) { EditorGUILayout.PropertyField(serializedObject.FindProperty("receiver")); }
            else if (sender.receiverType == ReceiverType.ByName) { EditorGUILayout.PropertyField(serializedObject.FindProperty("receiverName")); }
            else if (sender.receiverType == ReceiverType.ByTag) { EditorGUILayout.PropertyField(serializedObject.FindProperty("receiverTag")); }

            EditorGUILayout.LabelField("Message", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("randomizeMessage"));
            if (sender.randomizeMessage) { EditorGUILayout.PropertyField(serializedObject.FindProperty("messages"),true); }
            else { EditorGUILayout.PropertyField(serializedObject.FindProperty("message")); }            
            

            serializedObject.ApplyModifiedProperties();
        }
    }

#endif

}
