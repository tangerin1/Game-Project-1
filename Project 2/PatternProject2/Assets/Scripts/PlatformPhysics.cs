using UnityEngine;

public class PlatformPhysics : MonoBehaviour
{
    private void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag("Player"))
        {
            player.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if (player.CompareTag("Player"))
        {
            player.transform.parent = null;
        }
    }
}
