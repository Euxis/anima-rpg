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

    [SerializeField] private Rigidbody2D rbPlayer;

    protected override void Awake()
    {
        base.Awake();

        if (!TryGetComponent<DialogueSequence>(out npcDialogue))
        {
            Debug.LogError("No DialogueSequence attached");
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
            this.SelectHighlight(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.SelectHighlight(false);
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
