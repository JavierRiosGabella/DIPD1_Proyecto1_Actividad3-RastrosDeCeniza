using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UP_WaveMovement : MonoBehaviour {

    public enum MovementType
    {
        Horizontal, Vertical
    }

    [SerializeField] MovementType movementType = MovementType.Horizontal;
    [SerializeField] float amplitude = 1;
    [SerializeField] float frequency = 1;

    float randomTime;
    float lastIncrement;
    Rigidbody cmpRigidbody;

    void Awake()
    {
        randomTime = Mathf.PI * 2 * Random.Range(0, 1 / frequency);
        cmpRigidbody = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        lastIncrement = CalculateOscilation();
    }

    void FixedUpdate () {
        Vector3 position = transform.position;
        if(movementType == MovementType.Horizontal)
        {
            float newIncrement = CalculateOscilation();
            position.x += newIncrement - lastIncrement;
            lastIncrement = newIncrement;
        }
        else if(movementType == MovementType.Vertical)
        {
            float newIncrement = CalculateOscilation();
            position.y += newIncrement - lastIncrement;
            lastIncrement = newIncrement;
        }
        cmpRigidbody.MovePosition(position);
	}

    float CalculateOscilation()
    {
        return amplitude * Mathf.Sin(frequency * (Time.time + randomTime));
    }
}
