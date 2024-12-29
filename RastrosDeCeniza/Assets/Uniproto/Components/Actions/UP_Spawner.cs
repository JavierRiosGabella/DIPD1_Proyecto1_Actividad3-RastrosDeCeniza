using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UP_Spawner : MonoBehaviour {
   
    public enum AreaType { Rectangle, Circle, Points }

    [SerializeField] bool randomize = false;
    [SerializeField] GameObject prefab = null;
    [SerializeField] GameObject[] prefabs = null;

    [SerializeField] AreaType areaType = AreaType.Rectangle;
    [SerializeField] Vector2 rectangleSize = Vector2.zero;
    [SerializeField] float circleRadius = 1;
    [SerializeField] bool useCircunferenceOnly = true;
    [TagSelector,SerializeField] string pointsTag = null;
    [SerializeField] bool visitAllPoints = false;

    [SerializeField] bool reparentToSpawner = false;

    [SerializeField] bool avoidOverlap = false;
    [SerializeField] float overlapRadius = 1;
    [SerializeField] LayerMask overlapLayers;


    GameObject[] allPoints;
    List<GameObject> nextSpawnPoints;

    public void Start()
    {        
        if(areaType == AreaType.Points)
        {
            allPoints = GameObject.FindGameObjectsWithTag(pointsTag);
            nextSpawnPoints = new List<GameObject>(allPoints);
        }
    }

    public void UP_Instantiate()
    {
        Transform padre = reparentToSpawner ? this.transform : null;        
        GameObject prefabElegido = ChosePrefab();
        if(prefabElegido != null)
        {
            Vector2 posicionAleatoria;
            if(avoidOverlap) { posicionAleatoria = RandomPositionWithoutOverlap(); }
            else { posicionAleatoria = RandomPosition(); }
            
            GameObject newGO = Instantiate(prefabElegido, posicionAleatoria, this.transform.rotation, padre);
            ApplyNegativeScale(newGO);
        }
    }

    void ApplyNegativeScale(GameObject newGO)
    {
        if(this.transform.lossyScale.x < 0)
        {
            Vector3 newGOScale = newGO.transform.localScale;
            newGOScale.x = -Mathf.Abs(newGOScale.x);
            newGO.transform.localScale = newGOScale;
        }
    }

    private Vector2 RandomPositionWithoutOverlap()
    {
        int tries = 10;
        bool collision = true;
        Vector3 randomPosition;
        do
        {
            tries -= 1;
            randomPosition = RandomPosition();
            collision = Physics2D.OverlapCircle(randomPosition, overlapRadius);
        }
        while (collision && tries > 0);        
        return randomPosition;
    }

    private Vector2 RandomPosition()
    {
        Vector3 randomPosition = this.transform.position;
        if (areaType == AreaType.Rectangle)
        {
            randomPosition = RandomPositionInRectangle();
        }
        else if (areaType == AreaType.Circle)
        {
            randomPosition = RandomPositionInCircle();
        }
        else if(areaType == AreaType.Points)
        {
            randomPosition = RandomPositionInPoints();
        }
        return randomPosition;
    }

    private Vector2 RandomPositionInCircle()
    {
        Vector2 initialPosition = this.transform.position;
        Vector2 randomDistance = Random.insideUnitCircle;
        if (useCircunferenceOnly) { randomDistance.Normalize(); }
        randomDistance *= circleRadius;

        Vector2 randomPosition = initialPosition + randomDistance;        
        return randomPosition;
    }

    private Vector2 RandomPositionInRectangle()
    {
        Vector2 initialPosition = this.transform.position;
        Vector2 randomDistance = new Vector2(
                        x: Random.Range(-rectangleSize.x / 2, rectangleSize.x / 2),
                        y: Random.Range(-rectangleSize.y / 2, rectangleSize.y / 2));

        Vector2 randomPosition = initialPosition + randomDistance;
        return randomPosition;
    }

    private Vector2 RandomPositionInPoints()
    {
        Vector3 position = this.transform.position;

        if(nextSpawnPoints != null && nextSpawnPoints.Count > 0)
        {
            int indiceAleatorio = Random.Range(0, nextSpawnPoints.Count);
            position = nextSpawnPoints[indiceAleatorio].transform.position;
            if(visitAllPoints)
            {
                nextSpawnPoints.RemoveAt(indiceAleatorio);
                if (nextSpawnPoints.Count == 0) { nextSpawnPoints.AddRange(allPoints); }
            }
        }

        return position;        
    }

    private GameObject ChosePrefab()
    {
        GameObject chosenPrefab = prefab;

        if (randomize && prefabs != null && prefabs.Length > 0)
        {
            int iRandom = Random.Range(0, prefabs.Length);
            chosenPrefab = prefabs[iRandom];
        }

        return chosenPrefab;
    }

    void OnDrawGizmos()
    {
        if (areaType == AreaType.Rectangle)
        {
            Gizmos.color = Color.red;            
            Gizmos.DrawWireCube(
                center: this.transform.position, 
                size: rectangleSize
            );
        }
        else if (areaType == AreaType.Circle)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(
                center: this.transform.position,
                radius: circleRadius);            
        }
    }

#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(UP_Spawner))]
    public class CustomEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            UP_Spawner generador = target as UP_Spawner;

            serializedObject.Update();
                        
            EditorGUILayout.LabelField("Prefab", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(serializedObject.FindProperty("randomize"));
            
            if(!generador.randomize) { EditorGUILayout.PropertyField(serializedObject.FindProperty("prefab")); }
            else { EditorGUILayout.PropertyField(serializedObject.FindProperty("prefabs"),true); }

            
            EditorGUILayout.LabelField("Area", EditorStyles.boldLabel);

            AreaType tipoAreaPrevia = generador.areaType;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("areaType"));
            
            if (generador.areaType != tipoAreaPrevia) { SceneView.RepaintAll(); }
            if (generador.areaType == AreaType.Rectangle)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("rectangleSize"));
            }
            else if(generador.areaType == AreaType.Circle)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("circleRadius"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("useCircunferenceOnly"));                
            }
            else if(generador.areaType == AreaType.Points)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("pointsTag"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("visitAllPoints"));                
            }

            EditorGUILayout.LabelField("Collisions", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("avoidOverlap"));            
            if(generador.avoidOverlap)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("overlapRadius"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("overlapLayers"));
            }

            EditorGUILayout.LabelField("Parenting", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("reparentToSpawner"));

            serializedObject.ApplyModifiedProperties();
        }

    }

    

#endif
}
