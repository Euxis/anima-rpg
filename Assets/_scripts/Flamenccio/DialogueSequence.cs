using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;

public class DialogueSequence : MonoBehaviour
{
    [Tooltip("The lines this interactable can say"), SerializeField] private List<LocalizedString> dialogues = new();
    public int CurrentLine { get; set; }

    /// <summary>
    /// Retrieves specified dialogue line of this interactable object
    /// </summary>
    /// <param name="line">Line number</param>
    public string GetDialogueLine(int line)
    {
        if (line > dialogues.Count || line < 0)
        {
            Debug.LogError($"{gameObject.name}: line number {line} not available");
            return null;
        }

        return dialogues[line].GetLocalizedString();
    }

    /// <summary>
    /// Gets the current line
    /// </summary>
    /// <returns></returns>
    public string GetCurrentLine()
    {
        return dialogues[CurrentLine].GetLocalizedString();
    }

    /// <summary>
    /// Advances the dialogue
    /// </summary>
    /// <returns>
    /// <b>false</b> if unable to advance further; <b>true</b> otherwise
    /// </returns>
    public bool AdvanceDialogue()
    {
        if (CurrentLine >= dialogues.Count)
        {
            return false;
        }

        CurrentLine++;
        return true;
    }

    /// <summary>
    /// Removes all dialogue lines
    /// </summary>
    /// <returns>
    /// The previous lines that were cleared
    /// </returns>
    public List<LocalizedString> ClearLines()
    {
        var oldList = new List<LocalizedString>(dialogues);
        dialogues.Clear();
        return oldList;
    }

    /// <summary>
    /// Clears the dialogue list and replaces them with new lines
    /// </summary>
    /// <returns>Old lines that were cleared</returns>
    public List<LocalizedString> SetLines(List<LocalizedString> newLines)
    {
        var oldLines = ClearLines();
        dialogues = newLines;
        return oldLines;
    }
}
