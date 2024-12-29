using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UP_LookAtMovement : MonoBehaviour {

    public enum LookDirection
    {
        Up,
        Right,
        Down,
        Left        
    }

    [SerializeField] bool lookForTarget = false;
    [SerializeField] GameObject target;
    [SerializeField] string targetName = null;
    [SerializeField] bool useMaxSpeed = false;
    [SerializeField] float maxSpeed = 90;

    [SerializeField] LookDirection initialDirection = LookDirection.Up;
    float initialRotation { get { return (int)initialDirection * 90; } }

    void Start()
    {
        RefreshTarget();
    }

    void RefreshTarget()
    {
        if (lookForTarget)
        {
            this.target = GameObject.Find(targetName);
        }
    }

    void Update () {
        if(target == null)
        {
            RefreshTarget();
        }
        if(target != null)
        {
            Vector3 direction = target.transform.position - this.transform.position;
            float angle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(initialRotation + angle, Vector3.forward);
            if (useMaxSpeed)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, maxSpeed * Time.deltaTime);
            }
            else
            {
                transform.rotation = targetRotation;
            }
        }
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(UP_LookAtMovement))]
    public class CustomEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            UP_LookAtMovement rotateTowards = target as UP_LookAtMovement;

            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("lookForTarget"));
            if (rotateTowards.lookForTarget)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("targetName"));                
            }
            else
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("target"));                
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("initialDirection"));            

            serializedObject.ApplyModifiedProperties();
        }
    }

#endif
}
