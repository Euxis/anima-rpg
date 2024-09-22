using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlManager : MonoBehaviour
{
    public PlayerInput playerInput;

    private InputActionMap movementMap;

    // <summary>
    // Switches to movement action map
    // </summary>
    public void ToMovement() {
        playerInput.SwitchCurrentActionMap("default");
    }

    public void ToDialogue() {
        playerInput.SwitchCurrentActionMap("dialogue");
    }


}
