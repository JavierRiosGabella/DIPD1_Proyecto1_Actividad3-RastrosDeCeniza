using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UP_LineOfSightEvent : MonoBehaviour
{
    [SerializeField] bool lookForTarget = false;
    [SerializeField] GameObject target = null;
    [SerializeField] string targetName = null;
    [SerializeField] float viewDistance = 100;
    [SerializeField] float viewAngle = 90;
    [SerializeField] LayerMask ignoreLayers = 0;

    [SerializeField] UP_NoArgsUnityEvent onTargetDetected = null;
    [SerializeField] UP_NoArgsUnityEvent onTargetLost = null;

    bool isTargetVisible = false;
    RaycastHit2D[] hitInfo = new RaycastHit2D[1];

    // Start is called before the first frame update
    void Start()
    {
        if (lookForTarget) { this.target = GameObject.Find(targetName); }
        Physics2D.queriesStartInColliders = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(lookForTarget && target == null) { this.target = GameObject.Find(targetName); }
        if(target != null) {
            Vector3 targetPosition = target.transform.position;
            Vector3 targetDirection = targetPosition - this.transform.position;

            LayerMask layers = Physics2D.AllLayers;
            layers = layers & ~ignoreLayers;

            ContactFilter2D filter = new ContactFilter2D();            
            filter.SetLayerMask(layers);
                       
            bool canSeeTarget = false;
            float angleToTarget = Vector2.Angle(this.transform.up, targetDirection);
            if(angleToTarget < viewAngle / 2)
            {
                Debug.DrawRay(this.transform.position, targetDirection * viewDistance, Color.red);
                int nImpacts = Physics2D.Raycast(this.transform.position, targetDirection, filter, hitInfo, viewDistance);
                if (nImpacts > 0)
                {
                    RaycastHit2D impact = hitInfo[0];
                    if (impact.rigidbody?.gameObject == target)
                    {
                        canSeeTarget = true;
                    }
                }
            }            

            if(!isTargetVisible && canSeeTarget)
            {
                isTargetVisible = true;
                if(onTargetDetected != null) { onTargetDetected.Invoke();  }
            }
            else if(isTargetVisible && !canSeeTarget)
            {
                isTargetVisible = false;
                if(onTargetLost != null) { onTargetLost.Invoke(); }
            }
        }
    }

}
