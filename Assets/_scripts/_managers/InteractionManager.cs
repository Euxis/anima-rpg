using UnityEngine;

/// <summary>
/// This class purely exists to hold the current looked at GameObject detected by the player's boxcast
/// </summary>
public class InteractionManager : MonoBehaviour
{
    private RaycastHit2D hit;

    public static InteractionManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            // Set the static instance to this instance
            Instance = this;

            // Optionally, make this instance persistent across scenes
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetHit(RaycastHit2D input)
    {
        hit = input;
    }

    public void ClearHit()
    {
        hit = default;
    }

    public RaycastHit2D GetHit()
    {
        return hit;
    }
}