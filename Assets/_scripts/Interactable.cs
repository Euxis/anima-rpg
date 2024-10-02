using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // GENERIC INTERACTABLE CLASS

    private SpriteRenderer spriteRenderer;

    private new Renderer renderer;

    private Material material;

    public abstract void Interact();

    private void Update()
    {
        DetectCollision();
    }

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

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        renderer = GetComponent<Renderer>();
        material = renderer.material;
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