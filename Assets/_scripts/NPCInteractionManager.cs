using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Checks if its being hit by a raycast/boxcast
/// </summary>
public class NPCInteractionManager : MonoBehaviour
{
    [SerializeField] private UnityEvent onHit;

    public void OnBoxcastHit() {
        onHit?.Invoke();
    }

    public void OnBoxCastExit() { 
        
    
    }
}
