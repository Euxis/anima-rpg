using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.PropertyVariants.TrackedProperties;
using UnityEngine.Localization.Tables;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    public TMP_Text textbox;

    public LocalizedStringTable locTable;
    public StringTable stringTable;

    [SerializeField]
    private bool isTableLoaded=false;

    private void Awake()
    {
        // Asynchronously load the StringTable from the LocalizedStringTable
        locTable.GetTableAsync().Completed += (asyncOperation) =>
        {
            if (asyncOperation.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                stringTable = asyncOperation.Result; // Now you have access to the StringTable
                Debug.Log("String Table Loaded Successfully");
                isTableLoaded = true;
            }
            else
            {
                Debug.LogError("Failed to load String Table");
            }
        };
    }

    public void DoDialogue(string[] s) {

        string subs = s[0];
        StringTableEntry entry;

        Debug.Log("isTableLoaded: " + isTableLoaded);

        if (!isTableLoaded)
        {
            Debug.LogError("String Table not loaded yet!");
            return; // Prevent null reference exceptions
        }
        else { 
            entry = stringTable.GetEntry(subs);

        }

        // display string one after another
        Debug.Log(s[0]);
        

        if (entry != null)
        {
            Debug.Log(entry.LocalizedValue);
        }
        else
        {
            Debug.LogError("Entry not found.");
        }

        Debug.Log("here: " + entry.LocalizedValue);
        textbox.text = entry.LocalizedValue;
    }
}
