using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIalogueTree : MonoBehaviour
{
    [Tooltip("Tree of dialogue nodes"), SerializeField] private List<DialogueNode> tree = new();
}
