using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextboxManager textboxManager;

    [SerializeField] private ActionMapManager actionMapManager;

    [SerializeField] private CameraFollow cameraFollow;

    public DialogueSequence sequence { get; set; }

    public void StartDialogue()
    {
        cameraFollow.DialogueMode(InteractionManager.Instance.GetHit().collider.gameObject.transform);
        
    }

    /// <summary>
    /// Receives interact button from dialogue action map to advance dialogue
    /// <para> If its a new dialogue it will display the first line</para>
    /// </summary>
    /// <param name="context"></param>
    public void AdvanceDialogue(InputAction.CallbackContext context)
    {
        textboxManager.DialogueBoxVisibility(true);
        if (context.performed)
        {
            if (sequence.IsAtEnd() && !textboxManager.IsTyping())
            {
                sequence.hasTalked = true;
                ExitDialogue();
                return;
            }

            if (textboxManager.IsTyping())
            {
                textboxManager.FinishDialogue();
            }
            else
            {
                textboxManager.ClearText();
                textboxManager.DoDialogue(sequence.GetCurrentLine());

                // just to really make sure, check again if we're at the end of dialogue
                if (!sequence.AdvanceDialogue())
                {
                    sequence.hasTalked = true;
                    ExitDialogue();
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Cancels any dialogue
    /// </summary>
    /// <param name="context"></param>
    public void CancelDialogue(InputAction.CallbackContext context)
    {
        ExitDialogue();
        return;
    }

    /// <summary>
    /// Resets textbox and gives control back to player
    /// </summary>
    private void ExitDialogue()
    {
        cameraFollow.MovementMode();

        actionMapManager.ToMovement();
        sequence.ResetCurrentLine();
        textboxManager.ClearText();
        textboxManager.DialogueBoxVisibility(false);
    }
}