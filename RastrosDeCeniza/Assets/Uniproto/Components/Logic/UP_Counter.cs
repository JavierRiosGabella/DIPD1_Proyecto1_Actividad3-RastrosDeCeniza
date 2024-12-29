using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class UP_Counter : MonoBehaviour {

    public enum Comparison
    {
        LessOrEqual,
        Equal,
        GreaterOrEqual        
    }

    [SerializeField] int counter;
    [SerializeField] int multiplier = 1;
    [SerializeField] bool alertIfReachesValue = false;
    [SerializeField] int targetValue = 0;
    [SerializeField] Comparison comparison = Comparison.Equal;
    [SerializeField] UP_NoArgsUnityEvent onValueReached = null;

    public int Counter
    {
        get { return counter; }
    }

    public void UP_SetMultiplier(int value)
    {
        this.multiplier = value;
    }

    public void UP_IncreaseMultiplier(int increase)
    {
        multiplier += increase;
    }

    public void UP_SetCounter(int value)
    {
        this.counter = value;
        CheckAlert();
    }

    public void UP_IncreaseCounter(int increase)
    {
        counter += increase * multiplier;
        CheckAlert();
    }

    private void CheckAlert()
    {
        if (alertIfReachesValue)
        {
            switch (comparison)
            {
                case Comparison.Equal:
                    if (counter == targetValue) { SendEvent(); }
                    break;
                case Comparison.GreaterOrEqual:
                    if (counter >= targetValue) { SendEvent(); }
                    break;
                case Comparison.LessOrEqual:
                    if (counter <= targetValue) { SendEvent(); }
                    break;
            }
        }
    }

    void SendEvent()
    {
        if (onValueReached != null) { onValueReached.Invoke(); }
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(UP_Counter))]
    public class CustomEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            UP_Counter cont = target as UP_Counter;

            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("counter"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("multiplier"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("alertIfReachesValue"));
            if (cont.alertIfReachesValue)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("targetValue"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("comparison"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onValueReached"));                
            }

            serializedObject.ApplyModifiedProperties();

        }
    }

#endif

}
