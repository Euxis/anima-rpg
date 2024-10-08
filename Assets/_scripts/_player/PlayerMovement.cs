using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rbPlayer;

    // movement variables
    private float moveSpeed = 5.0f;
    private float runSpeed = 7.0f;
    private Vector2 direction;
    private bool isRun = false;

    private void FixedUpdate()
    {
        if (isRun)
        {
            rbPlayer.velocity = direction * runSpeed;
        }
        else { 
            rbPlayer.velocity = direction * moveSpeed;
        }
    }

    /// <summary>
    /// Reads Vector2 value from player input
    /// </summary>
    /// <param name="context"></param>
    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// Gets run input
    /// </summary>
    /// <param name="context"></param>
    public void GetRun(InputAction.CallbackContext context) {
        if (context.canceled)
        {
            isRun = false;
        }
        else {
            isRun = true;
        }
    }
}
