using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;
using UnityEngine.Localization.PropertyVariants.TrackedProperties;
using UnityEngine.Localization.Tables;

public class TextboxManager : MonoBehaviour
{
    private enum TextState { Paused, Typing}

    [SerializeField] private TextState textState = TextState.Paused;

    [SerializeField] public TMP_Text textbox;

    [SerializeField] private RectTransform panel;

    public static TextboxManager Instance;

    private float textWaitTime = 0.02f;

    private string inputText;

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

    public bool IsTyping() {
        if (textState == TextState.Typing)
        {
            return true;
        }
        else {
            return false;
        }
    }

    /// <summary>
    /// Shows given text on screen
    /// <para>
    /// Also makes dialogue box visible
    /// </para>
    /// </summary>
    /// <param name="text"></param>
    public void DoDialogue(string text)
    {
        inputText = text;
        StopAllCoroutines();
        textState = TextState.Typing;
        ClearText();
        DialogueBoxVisibility(true);
        StartCoroutine(TypeText(text));       
    }

    public void FinishDialogue() {
        StopAllCoroutines();
        ClearText();
        textbox.text = inputText;
        textState = TextState.Paused;
    }

    /// <summary>
    /// Clears textbox
    /// </summary>
    public void ClearText()
    {
        StopAllCoroutines();
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
        
        panel.gameObject.SetActive(visible);
        textbox.gameObject.SetActive(visible);
    }

    /// <summary>
    /// Types out text character by character
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    private IEnumerator TypeText(string t) {
        
        foreach (char c in t) {
            if (c == ',' || c == '.' || c == '!')
                textWaitTime = 0.5f;
            else
                textWaitTime = 0.02f;

            textbox.text += c;
            yield return new WaitForSeconds(textWaitTime);
            
        }
        textState = TextState.Paused;
    }
}
