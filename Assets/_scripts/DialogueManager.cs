using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;
using UnityEngine.Localization.PropertyVariants.TrackedProperties;
using UnityEngine.Localization.Tables;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    public TMP_Text textbox;

    public static DialogueManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    /// <summary>
    /// Shows given text on screen
    /// <para>
    /// Also makes dialogue box visible
    /// </para>
    /// </summary>
    /// <param name="text"></param>
    public void DisplayDialogue(string text)
    {
        DialogueBoxVisibility(true);
        textbox.text = text;
    }

    /// <summary>
    /// Clears textbox
    /// </summary>
    public void ClearText()
    {
        textbox.text = string.Empty;
    }

    /// <summary>
    /// Changes the visibility of the textbox
    /// <para>
    /// Also clears the textbox
    /// </para>
    /// </summary>
    /// <param name="visible"></param>
    public void DialogueBoxVisibility(bool visible)
    {
        ClearText();
        textbox.gameObject.SetActive(visible);
    }

    public void AdvanceDialogue(InputAction.CallbackContext context) { 
        
    }
}
