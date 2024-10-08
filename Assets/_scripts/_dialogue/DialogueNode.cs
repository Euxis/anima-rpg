using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueNode : MonoBehaviour
{
    public string key;
    public List<DialogueChoice> choices;
}

[System.Serializable]
public class DialogueChoice {
    public string key;
    public int nextNode;
}