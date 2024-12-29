using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
struct RandomRange
{
    public RandomRange(float min, float max)
    {
        this.min = min;
        this.max = max;
    }

    public float min;
    public float max;
}

public class UP_Timer : MonoBehaviour {

    [SerializeField] float initialTime = 1;
    [SerializeField] RandomRange randomInitialTime = new RandomRange(0, 1);
    [SerializeField] bool repeat = false;
    [SerializeField] float repeatTime = 1;
    [SerializeField] RandomRange randomRepeatTime = new RandomRange(0,1);
    [SerializeField] bool randomizeTimes = false;
    [SerializeField] bool autoStart = true;
    [SerializeField] UP_NoArgsUnityEvent onTimeUp = new UP_NoArgsUnityEvent();

    bool started = false;
    bool restartOnEnable = false;

    float InitialTime => !randomizeTimes ? initialTime : 
        Random.Range(randomInitialTime.min, randomInitialTime.max);

    float RepeatTime => !randomizeTimes ? repeatTime : 
        Random.Range(randomRepeatTime.min, randomRepeatTime.max);

    

    void Awake()
    {
        if(autoStart)
        {
            UP_StartTimer();
        }
    }

    void OnEnable()
    {
        if(restartOnEnable)
        {
            restartOnEnable = false;
            UP_StartTimer();
        }
    }

    void OnDisable()
    {
        if(started) { restartOnEnable = true; }
        UP_StopTimer();
    }

    public void UP_StartTimer()
    {
        if(!started)
        { 
            started = true;            
            Invoke("TimerEvent", InitialTime);
            
        }
    }

    public void UP_StopTimer()
    {
        if(started)
        {
            started = false;
            CancelInvoke();
        }        
    }

    void TimerEvent()
    {
        onTimeUp.Invoke();
        if(repeat && started)
        {
            Invoke("TimerEvent", RepeatTime);
        }
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(UP_Timer))]
    public class CustomEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            UP_Timer temp = target as UP_Timer;

            serializedObject.Update();

            EditorGUILayout.LabelField("Time", EditorStyles.boldLabel);
            string propTiempoInicial = temp.randomizeTimes ? "randomInitialTime" : "initialTime";
            EditorGUILayout.PropertyField(serializedObject.FindProperty(propTiempoInicial), new GUIContent("Initial Time"),true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("repeat"));            
            if(temp.repeat)
            {
                string propTiempoRecurrente = temp.randomizeTimes ? "randomRepeatTime" : "repeatTime";
                EditorGUILayout.PropertyField(serializedObject.FindProperty(propTiempoRecurrente), new GUIContent("Repeat Time"),true);                
            }
            EditorGUILayout.LabelField("Random", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("randomizeTimes"), new GUIContent("Random Times"));
            EditorGUILayout.LabelField("Start", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("autoStart"));
            EditorGUILayout.LabelField("Events", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onTimeUp"));

            serializedObject.ApplyModifiedProperties();
        }
    }

#endif

}
