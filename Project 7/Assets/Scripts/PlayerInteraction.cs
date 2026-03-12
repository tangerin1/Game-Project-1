using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 3f;

    private TapeRecorder currentRecorder;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        TapeRecorder recorderLookingAt = null;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            recorderLookingAt = hit.collider.GetComponentInParent<TapeRecorder>();
        }

        if (currentRecorder != null && currentRecorder != recorderLookingAt)
        {
            currentRecorder.PlayerHover(false);
        }

        if (recorderLookingAt != null)
        {
            currentRecorder = recorderLookingAt;
            currentRecorder.PlayerHover(true);
        }
        else
        {
            currentRecorder = null;
        }
    }
}