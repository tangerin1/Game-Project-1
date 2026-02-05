using UnityEngine;

public class ArrowDamage : MonoBehaviour
{
    public float damage = 10f;

    void OnTriggerEnter(Collider other)
    {
        DragonAI dragon = other.GetComponent<DragonAI>();
        if (dragon != null)
        {
            dragon.TakeDamage(damage);
        }
    }
}