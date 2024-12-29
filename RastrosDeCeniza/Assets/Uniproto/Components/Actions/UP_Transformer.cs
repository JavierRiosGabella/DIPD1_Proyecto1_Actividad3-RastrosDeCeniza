using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class UP_Transformer : MonoBehaviour {

    public enum MovementType
    {
        Local, Global
    }

    [SerializeField] MovementType movementType = MovementType.Local;
    Rigidbody2D cmpRigidbody;


    void Awake()
    {
        cmpRigidbody = GetComponent<Rigidbody2D>();
    }

    public void UP_SetPositionX(float posX)
    {
        if (movementType == MovementType.Global)
        {
            Vector3 currentPos = this.transform.position;
            currentPos.x = posX;
            this.transform.position = currentPos;
        }
        else
        {
            Vector3 currentPos = this.transform.localPosition;
            currentPos.x = posX;
            this.transform.localPosition = currentPos;
        }
    }

    public void UP_SetPositionY(float posY)
    {
        if (movementType == MovementType.Global)
        {
            Vector3 currentPos = this.transform.position;
            currentPos.y = posY;
            this.transform.position = currentPos;
        }
        else
        {
            Vector3 currentPos = this.transform.localPosition;
            currentPos.y = posY;
            this.transform.localPosition = currentPos;
        }
    }

    public void UP_SetRotation(float rotation)
    {
        if (movementType == MovementType.Global)
        {
            Vector3 currentRotation = this.transform.eulerAngles;
            currentRotation.z = rotation;
            this.transform.eulerAngles = currentRotation;
        }
        else
        {
            Vector3 currentRotation = this.transform.localEulerAngles;
            currentRotation.z = rotation;
            this.transform.localEulerAngles = currentRotation;
        }
    }

    public void UP_TranslateX(float movement)
    {
        bool global = (movementType == MovementType.Global);
        Vector3 direccion = global ? Vector3.right : this.transform.right;
        cmpRigidbody.MovePosition(this.transform.position + direccion * movement);        
    }

    public void UP_TranslateY(float movement)
    {
        bool global = (movementType == MovementType.Global);
        Vector3 direccion = global ? Vector3.up : this.transform.up;
        cmpRigidbody.MovePosition(this.transform.position + direccion * movement);
    }

    public void UP_Rotate(float rotation)
    {
        cmpRigidbody.MoveRotation(this.transform.eulerAngles.z + rotation);
    }

    public void UP_AddForceY(float force)
    {
        bool global = (movementType == MovementType.Global);
        Vector3 direction = global ? Vector3.up : this.transform.up;
        cmpRigidbody.AddForce(direction * force);
    }

    public void UP_AddForceX(float force)
    {
        bool global = (movementType == MovementType.Global);
        Vector3 direction = global ? Vector3.right : this.transform.right;
        float invert = transform.lossyScale.x < 0 ? -1 : 1;
        cmpRigidbody.AddForce(direction * force * invert);
    }

    public void UP_AddTorque(float torque)
    {
        cmpRigidbody.AddTorque(torque);
    }

    public void UP_SetVelocityX(float velocityX)
    {
        if (movementType == MovementType.Local)
        {
            Vector2 currentVelocity = cmpRigidbody.velocity;
            Vector2 localCurrentVelocity = this.transform.InverseTransformVector(currentVelocity);
            localCurrentVelocity.x = velocityX;
            cmpRigidbody.velocity = this.transform.TransformVector(localCurrentVelocity);
        }
        else
        {
            Vector2 currentVelocity = cmpRigidbody.velocity;
            currentVelocity.x = velocityX;
            cmpRigidbody.velocity = currentVelocity;
        }
    }

    public void UP_SetVelocityY(float velocityY)
    {
        if (movementType == MovementType.Local)
        {
            Vector2 currentVelocity = cmpRigidbody.velocity;
            Vector2 localCurrentVelocity = this.transform.InverseTransformVector(currentVelocity);
            localCurrentVelocity.y = velocityY;
            cmpRigidbody.velocity = this.transform.TransformVector(localCurrentVelocity);
        }
        else
        {
            Vector2 currentVelocity = cmpRigidbody.velocity;
            currentVelocity.y = velocityY;
            cmpRigidbody.velocity = currentVelocity;
        }
    }    

    public void UP_SetAngularVelocity(float angularVelocity)
    {
        cmpRigidbody.angularVelocity = angularVelocity;
    }
    
    public void UP_InvertScaleX(bool invert)
    {
        Vector3 scale = this.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (invert ? -1 : 1);
        this.transform.localScale = scale;
    }

}

