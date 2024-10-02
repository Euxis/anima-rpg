using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // GENERIC INTERACTABLE CLASS

    private new Renderer renderer;

    private Material material;

    public abstract void Interact();

    private void Update()
    {
        DetectCollision();
    }
    protected virtual void Awake()
    {
        renderer = GetComponent<Renderer>();
        material = renderer.material;
    }

    /// <summary>
    /// Outlines the object if its being looked at by player
    /// </summary>
    private void DetectCollision()
    {
        if (InteractionManager.Instance.GetHit().collider != null)
        {
            if (InteractionManager.Instance.GetHit().collider.gameObject == gameObject)
            {
                SelectHighlight(true);
            }
            else
            {
                SelectHighlight(false);
            }
        }
        else
        {
            SelectHighlight(false);
        }
    }

    

    /// <summary>
    /// Changes the look of the sprite when its looked at
    /// <para>Will change later</para>
    /// </summary>
    /// <param name="b"></param>
    public void SelectHighlight(bool b)
    {
        if (b)
        {
            material.SetFloat("_OutlineEnabled", 1f);
        }
        else
        {
            material.SetFloat("_OutlineEnabled", 0f);
        }
    }
}