using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : Interactable
{
    public DialogueManager dialogueManager;
    public GameObject objDM;

    [SerializeField]
    public string[] s;

    public override void Interact()
    {
        // send text lines to dialogueManager
        dialogueManager.DoDialogue(s);
    }
}
