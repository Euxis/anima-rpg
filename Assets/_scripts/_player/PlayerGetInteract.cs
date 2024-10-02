using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Gets interact input from player and does stuff based on what its looking at
/// </summary>
public class PlayerGetInteract : MonoBehaviour
{
    [SerializeField] private ActionMapManager actionMapManager;
    public void GetInteract(InputAction.CallbackContext context) {
        if (InteractionManager.Instance.GetHit().collider != null) {
            if (InteractionManager.Instance.GetHit().collider.TryGetComponent<Interactable>(out Interactable output)) {
                actionMapManager.ToDialogue();
                output.Interact();
            }
        }
    }
}
