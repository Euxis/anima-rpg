using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPCInteractable : Interactable
{
    public GameObject objDM;
    private DialogueSequence npcDialogue;

    [SerializeField]
    private ControlManager controlManager;

    private InputAction cancel;

    


    protected override void Awake()
    {
        base.Awake();

        if (!TryGetComponent<DialogueSequence>(out npcDialogue))
        {
            Debug.LogError("No DialogueSequence attached");
            return;
        }
    }

    // <summary>
    // starts a dialogue with the player
    // won't stop until the player cancels or finishes the dialogue
    // </summary
    public override void Interact()
    {
        controlManager.ToDialogue();

        var line = npcDialogue.GetCurrentLine();
        DialogueManager.Instance.DisplayDialogue(line);
    }

    public void CancelDialogue() {
        DialogueManager.Instance.ClearText();
        DialogueManager.Instance.DialogueBoxVisibility(false);
        controlManager.ToMovement();
    }

    public void DoDialogue() {
        npcDialogue.AdvanceDialogue();
        var line = npcDialogue.GetCurrentLine();
        DialogueManager.Instance.DisplayDialogue(line);
    }
}
