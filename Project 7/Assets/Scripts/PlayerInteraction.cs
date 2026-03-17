using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 3f;

    private IInteractable currentInteractable;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        IInteractable interactableLookingAt = null;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            interactableLookingAt = hit.collider.GetComponentInParent<IInteractable>();
        }
        if (currentInteractable != null && currentInteractable != interactableLookingAt)
        {
            currentInteractable.OnHoverExit();
        }
        if (interactableLookingAt != null)
        {
            currentInteractable = interactableLookingAt;
            currentInteractable.OnHoverEnter();
        }
        else
        {
            currentInteractable = null;
        }
    }
}