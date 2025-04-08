using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float followSmooth = 0.2f;
    [SerializeField] float lookAheadDistance = 30f;
    [SerializeField] float minDistanceToTarget = 5f;
    [SerializeField] float verticalOffset = 1f;

    // no obligatorio
    [SerializeField] float rotationSmooth = 1f;
    [SerializeField] float lookAheadSmooth = 0.2f;
    
    private Vector3 currentVelocity;

    void FixedUpdate()
    {
        transform.LookAt(target.transform.position + lookAheadDistance * target.transform.forward);

        Vector3 targetPosition = target.position + minDistanceToTarget * -target.transform.forward + verticalOffset * Vector3.up;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, followSmooth);

        float vectorDistance = (target.position - transform.position).magnitude;
        
        if (vectorDistance < minDistanceToTarget)
        {
            Vector3 cameraTargetVector = target.position - transform.position;
            Vector3 newPosition = minDistanceToTarget / cameraTargetVector.magnitude * cameraTargetVector;
            transform.position = target.position - cameraTargetVector.normalized * minDistanceToTarget;
        }
    }
}
