using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;

    bool isFocus = false;
    Transform player;

    bool hasInteracted = false;

    private Outline outline;

    private void Start()
    {
        outline = GetComponent<Outline>();
        SetupOutline();
    }

    private void SetupOutline()
    {
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.black;
        outline.OutlineWidth = 5f;
        outline.enabled = false;
    }

    public void OnFocus(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    private void OnDrawGizmosSelected()
    {
        if(interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

    private void Update()
    {
        if(isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    public virtual void Interact()
    {
        // This method is meant to be overwritten
        Debug.Log("[Interactable] Interacting with " + transform.name);
    }

    #region HANDLING OUTLINE
    private void OnMouseEnter()
    {
        if (!GameManager.instance.IsGamePaused())
            outline.enabled = true;
    }

    private void OnMouseExit()
    {
        outline.enabled = false;
    }
    #endregion

}
