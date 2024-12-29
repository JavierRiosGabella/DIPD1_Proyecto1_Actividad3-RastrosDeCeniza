using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UP_CursorMovement : MonoBehaviour {

    [SerializeField] float cameraDistance = 10;
    Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update () {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = cameraDistance;
        this.transform.position = mainCamera.ScreenToWorldPoint(mousePosition);
	}
}
