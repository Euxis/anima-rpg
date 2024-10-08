using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPCInteractable : Interactable
{
    public GameObject objDM;
    private DialogueSequence npcDialogue;   
    
    [SerializeField] private DialogueManager dialogueManager;

    protected override void Awake()
    {
        base.Awake();

        if (!TryGetComponent<DialogueSequence>(out npcDialogue))
        {
            Debug.LogError("No DialogueSequence attached");
            return;
        }
    }

    /// <summary>
    /// Switches action map dialogue mode
    /// </summary>
    public override void Interact()
    {
        if (npcDialogue != null)
        {
            dialogueManager.sequence = npcDialogue;
            dialogueManager.StartDialogue();

        }
        else {
            Debug.LogError("No dialogue in sequence");
        }

    }
}
