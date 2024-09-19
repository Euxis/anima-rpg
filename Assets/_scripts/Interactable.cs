using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders.Simulation;

public class Interactable : MonoBehaviour
{
    // GENERIC INTERACTABLE CLASS

    private SpriteRenderer spriteRenderer;

    public virtual void Interact() {
        Debug.Log("basic interact");
    } 

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SelectHighlight(bool b) {
        if (b)
        {
            spriteRenderer.color = Color.red;
        }
        else {
            spriteRenderer.color = Color.white;
        }
    }
}
