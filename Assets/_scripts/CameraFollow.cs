using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform cameraTransform;
    [SerializeField]
    private Transform playerTransform;



    void Start()
    {
        // for readability
        cameraTransform = transform;
    }

    void Update()
    {
        cameraTransform.position = Vector2.Lerp(playerTransform.position, cameraTransform.position, 0.01f);
        
    }
}
