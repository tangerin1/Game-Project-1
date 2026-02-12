using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform player;
    public Transform[] teleportPoints;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        bool currentlyVisible = rend.isVisible;

        // teleport if not visible
        if (!currentlyVisible)
        {
            Teleport();
        }
    }

    void Teleport()
    {
        if (teleportPoints.Length == 0)
        {
            Debug.LogWarning("No teleport points assigned!");
            return;
        }

        int index = Random.Range(0, teleportPoints.Length);
        transform.position = teleportPoints[index].position;
    }
}