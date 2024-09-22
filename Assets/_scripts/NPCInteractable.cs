using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : Interactable
{
    public GameObject objDM;
    private DialogueSequence npcDialogue;

    [SerializeField]
    private ControlManager controlManager;

    


    protected override void Awake()
    {
        base.Awake();

        if (!TryGetComponent<DialogueSequence>(out npcDialogue))
        {
            Debug.LogError("No DialogueSequence attached");
            return;
        }
    }
    public override void Interact()
    {
        controlManager.ToDialogue();

        var line = npcDialogue.GetCurrentLine();
        DialogueManager.Instance.DisplayDialogue(line);
    }
}
