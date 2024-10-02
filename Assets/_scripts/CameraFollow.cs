using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    public enum CameraState { Movement, Dialogue}
    public CameraState state = CameraState.Movement;

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform playerTransform;

    private float zoomSpeed = 4f;  // Speed of zooming
    private float minZoom = 3f;    // Minimum zoom limit
    private float maxZoom = 5f;   // Maximum zoom limit
    private float zoomFactor = 2.0f;  // Sensitivity of zoom based on distance

    private new Camera camera;

    private Transform targetTransform;      // The transform to lerp between

    private float distance;         // distance between target and player

    private float targetZoom;

    void Start()
    {
        // for readability
        camera = GetComponent<Camera>();
        cameraTransform = transform;
        MovementMode();
    }

    void Update()
    {
        MoveCamera();   
    }

    /// <summary>
    /// Moves camera based on <parameter>CameraState</parameter>
    /// </summary>
    private void MoveCamera() {
        if(state == CameraState.Movement)
        {
            cameraTransform.position = Vector2.Lerp(cameraTransform.position, playerTransform.position, 0.01f);
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed);
        }
        else if (state == CameraState.Dialogue) {
            cameraTransform.position = Vector2.Lerp(cameraTransform.position, targetTransform.position, 0.05f);
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed);
        }
    }

    /// <summary>
    /// Focuses on the player with a smooth follow
    /// </summary>
    public void MovementMode() {
        state = CameraState.Movement;
        targetZoom = 5f;
    }

    /// <summary>
    /// Zooms in on the given transform being talked to while keeping the player
    /// in view
    /// </summary>
    public void DialogueMode(Transform target) {
        targetTransform = target;
        distance = Vector2.Distance(targetTransform.position, playerTransform.position);
        targetZoom = Mathf.Clamp(distance * zoomFactor, minZoom, maxZoom);
        state = CameraState.Dialogue;
    }
}
