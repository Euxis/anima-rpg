using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Gets the Interact script from player Boxcast
/// <para>If the player presses the interact button, will invoke it</para>
/// </summary>
public class PlayerRaycast : MonoBehaviour
{
    [SerializeField] private Transform transformPlayer;

    [SerializeField] private ActionMapManager controlManager;

    private ContactFilter2D contactFilter = new ContactFilter2D();

    private Vector2 playerDirection;

    private Vector2 lastDirection;

    private Vector2 boxSize = new Vector2(0.5f, 0.5f);

    private float boxLength = 2.3f;

    private LayerMask layerMask;

    // BoxCast results

    private RaycastHit2D boxHit;

    private RaycastHit2D lastBoxHit;

    [SerializeField] public Interactable scriptInteract;

    [SerializeField] private Interactable lastScriptInteract;

    [SerializeField] private RaycastHit2D[] boxHitArray = new RaycastHit2D[10];

    [SerializeField]
    private List<RaycastHit2D> boxHitList;



    private void Awake()
    {
        layerMask = LayerMask.GetMask("Interactables");

        contactFilter.SetLayerMask(layerMask);
    }

    private void Update()
    {
        DrawBoxCast(transformPlayer.position, lastDirection, boxSize, 0f, boxLength);

        HighlightInteractable();
    }

    /// <summary>
    /// If the boxcast sees an interactable, tell it to highlight itself
    /// <para>If the player looks away, reset the last interactable highlight</para>
    /// </summary>
    private void HighlightInteractable() {

        boxHit = Physics2D.BoxCast(transformPlayer.position, boxSize, 0f, lastDirection, boxLength);

        boxHitArray = Physics2D.BoxCastAll(transformPlayer.position, boxSize, 0f, lastDirection, boxLength);

        boxHitList = new List<RaycastHit2D>(boxHitArray);

        lastBoxHit = boxHit;


        foreach (RaycastHit2D hit in boxHitList) {
            Debug.Log(hit);
        }

        if (boxHit.collider != null && boxHit.collider.gameObject.TryGetComponent<Interactable>(out scriptInteract))
            scriptInteract.SelectHighlight(true);
        else if (scriptInteract != null) // Check if we need to reset the previous highlight
        {
            scriptInteract.SelectHighlight(false);
            scriptInteract = null; // Clear reference after resetting highlight
        }

        if (lastBoxHit.collider != boxHit.collider && lastBoxHit.collider.gameObject.TryGetComponent<Interactable>(out lastScriptInteract)) {
            lastScriptInteract.SelectHighlight(false);
        }

        foreach (RaycastHit2D hit in boxHitList)
        {
            if (hit.collider != boxHit.collider && hit.collider != null && hit.collider.gameObject.TryGetComponent<Interactable>(out Interactable tempInteractable))
            {
                Debug.Log("Cleared " + hit.collider.gameObject);
                tempInteractable.SelectHighlight(false);
            }

        }

        boxHitList.Clear();

        // only highlight the first hit
        /*
        if (boxHit.collider != null)
            lastBoxHit = boxHit;

        // if the boxcast sees an interactable, tell the interactable to highlight
        if (boxHit.collider != null && boxHit.collider.gameObject.TryGetComponent<Interactable>(out scriptInteract))
            scriptInteract.SelectHighlight(true);

        if (lastBoxHit.collider != null && lastBoxHit.collider != boxHit.collider) {
            if (lastBoxHit.collider.gameObject.TryGetComponent<Interactable>(out lastScriptInteract))
            {
                lastScriptInteract.SelectHighlight(false);
            }
        }

        // If the player looks away, reset the last interactable highlight
        if (scriptInteract != null && boxHit.collider == null)
        {
            scriptInteract.SelectHighlight(false);

        }*/
    }

    /// <summary>
    /// Changes Boxcast direction whenever the player changes direction
    /// </summary>
    /// <param name="context"></param>
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


    /// <summary>
    /// Sends boxcast from the direction in SendBoxcast()
    /// <para>If it hits something, get its Interact script and run it</para>
    /// </summary>
    /// <param name="context"></param>
    public void GetInteract(InputAction.CallbackContext context)
    {
        // Send out a boxcast from the last direction faced
        boxHit = Physics2D.BoxCast(transformPlayer.position, boxSize, 0f, lastDirection, boxLength);

        // If the interact button is pressed, check if the player is looking at anything
        if (context.performed && boxHit.collider != null && boxHit.collider.gameObject.TryGetComponent<Interactable>(out scriptInteract)) {
            controlManager.ToDialogue();
            scriptInteract.Interact();
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
