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
    [SerializeField]
    public TMP_Text textbox;

    [SerializeField]
    private RectTransform panel;

    public static TextboxManager Instance;

    private float textWaitTime = 0.1f;

    private bool slowTextSpeed = false;

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
        StopAllCoroutines();
        DialogueBoxVisibility(true);
        StartCoroutine(TypeText(text));
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
        ClearText();
        panel.gameObject.SetActive(visible);
        textbox.gameObject.SetActive(visible);
    }

    /// <summary>
    /// Types out text character by character
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    private IEnumerator TypeText(string t) {
        textWaitTime = 0.1f;
        slowTextSpeed = false;
        foreach (char c in t) {
            if (textWaitTime != 0.001 && !slowTextSpeed)
            {
                textWaitTime = Mathf.Lerp(textWaitTime, 0.001f, 0.2f);
            }
            else {
                slowTextSpeed = true;
                textWaitTime = Mathf.Lerp(0.1f, textWaitTime, 0.2f);
            }
            textbox.text += c;
            yield return new WaitForSeconds(textWaitTime);
            
        }
    }
}
