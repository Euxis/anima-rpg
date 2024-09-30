using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform cameraTransform;
    [SerializeField]
    private Transform playerTransform;

    public Transform dialogueTransform;     // the transform to use during dialogue sequences

    // TODO: Add a way to track a different object (i.e. zoom onto an NPC during dialogue)


    void Start()
    {
        // for readability
        cameraTransform = transform;
    }

    void Update()
    {
        MovementMode();   
    }

    /// <summary>
    /// Focuses on the player with a smooth follow
    /// </summary>
    private void MovementMode() {
        cameraTransform.position = Vector2.Lerp(cameraTransform.position, playerTransform.position, 0.01f);
    }

    /// <summary>
    /// Zooms in on the NPC being talked to while keeping the player
    /// in view
    /// </summary>
    private void DialogueScene() { 
    
    }
}
