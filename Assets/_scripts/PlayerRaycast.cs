using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRaycast : MonoBehaviour
{
    // script to cast a boxcast for interactions
    [SerializeField] private Transform transformPlayer;

    [SerializeField]
    private Vector2 playerDirection;
    private Vector2 boxSize = new Vector2(0.5f, 0.5f);
    private float boxLength = 2.3f;

    // band aid fix for now
    [SerializeField]
    private Vector2 lastDirection;

    private LayerMask layerMask;

    private RaycastHit2D boxHit;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    public Interactable scriptInteract;

    [SerializeField]
    private ActionMapManager controlManager;

    private void Awake()
    {
        layerMask = LayerMask.GetMask("Interactables");
    }

    private void Update()
    {
        DrawBoxCast(transformPlayer.position, lastDirection, boxSize, 0f, boxLength);

        boxHit = Physics2D.BoxCast(transformPlayer.position, boxSize, 0f, lastDirection, boxLength);

        // if the boxcast sees an interactable, tell the interactable to highlight
        if (boxHit.collider != null)
        {
            if(boxHit.collider.gameObject.TryGetComponent<Interactable>(out scriptInteract))
            {
                scriptInteract.SelectHighlight(true);
            }
        }

        // if the player looks away,
        if (scriptInteract != null && boxHit.collider == null) {

            // reset the last interactable highlight
            scriptInteract.SelectHighlight(false);
        }
    }

    // method for updating player direction
    public void SendBoxcast(InputAction.CallbackContext context)
    {
        // read vector from movement input
        playerDirection = context.ReadValue<Vector2>();

        // if the current player direction is DIFFERENT from the last direction faced, but NOT (0,0)
        // then update it
        if(playerDirection != lastDirection && playerDirection.x != 0 || playerDirection.y != 0)
        {
            lastDirection = playerDirection;
        }
    }

    public void GetInteract(InputAction.CallbackContext context)
    {
        // send out a boxcast from the last direction faced
        boxHit = Physics2D.BoxCast(transformPlayer.position, boxSize, 0f, lastDirection, boxLength);

        // if the interact button is pressed, check if the player is looking at anything
        if (context.performed) {
            if (boxHit.collider != null) {
                if (boxHit.collider.gameObject.TryGetComponent<Interactable>(out scriptInteract)) {
                    controlManager.ToDialogue();
                    scriptInteract.Interact();
                }
            }
        }
    }

    void DrawBoxCast(Vector2 origin, Vector2 direction, Vector2 size, float angle, float distance)
    {
        // Calculate the rotation of the box
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        // The four corners of the initial box
        Vector2 topLeft = (Vector2)(rotation * new Vector2(-size.x / 2, size.y / 2)) + origin;
        Vector2 topRight = (Vector2)(rotation * new Vector2(size.x / 2, size.y / 2)) + origin;
        Vector2 bottomLeft = (Vector2)(rotation * new Vector2(-size.x / 2, -size.y / 2)) + origin;
        Vector2 bottomRight = (Vector2)(rotation * new Vector2(size.x / 2, -size.y / 2)) + origin;

        // Offset the box corners by the cast direction and distance
        Vector2 castOffset = direction.normalized * distance;
        Vector2 topLeftEnd = topLeft + castOffset;
        Vector2 topRightEnd = topRight + castOffset;
        Vector2 bottomLeftEnd = bottomLeft + castOffset;
        Vector2 bottomRightEnd = bottomRight + castOffset;

        // Draw the starting box
        Debug.DrawLine(topLeft, topRight, Color.green);
        Debug.DrawLine(topRight, bottomRight, Color.green);
        Debug.DrawLine(bottomRight, bottomLeft, Color.green);
        Debug.DrawLine(bottomLeft, topLeft, Color.green);

        // Draw the ending box
        Debug.DrawLine(topLeftEnd, topRightEnd, Color.red);
        Debug.DrawLine(topRightEnd, bottomRightEnd, Color.red);
        Debug.DrawLine(bottomRightEnd, bottomLeftEnd, Color.red);
        Debug.DrawLine(bottomLeftEnd, topLeftEnd, Color.red);

        // Draw lines between the start and end positions (to show movement)
        Debug.DrawLine(topLeft, topLeftEnd, Color.yellow);
        Debug.DrawLine(topRight, topRightEnd, Color.yellow);
        Debug.DrawLine(bottomLeft, bottomLeftEnd, Color.yellow);
        Debug.DrawLine(bottomRight, bottomRightEnd, Color.yellow);
    }
}
