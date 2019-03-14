using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    // Game Object the camera is following
    private Transform target;
    public float smoothing = 5f;

    Vector3 offset;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        // The difference between where the camera starts and where the player
        offset = transform.position - target.position;  
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 targetCamPos = target.position + offset;
            // Move the camera to a new position
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);  
        }
    }
}
