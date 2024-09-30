using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPCInteractable : Interactable
{
    public GameObject objDM;
    private DialogueSequence npcDialogue;   
    

    [SerializeField]
    private DialogueManager dialogueManager;

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

    /// <summary>
    /// Switches action map dialogue mode
    /// </summary>
    public override void Interact()
    {

        dialogueManager.sequence = npcDialogue;
        dialogueManager.StartDialogue();
        
    }
}
