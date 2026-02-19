
using UnityEngine;

public class Donut : MonoBehaviour
{
    
    public static bool donutCollected = false;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            Destroy(gameObject);
            donutCollected = true;
        }
    }

    public bool getDonutCollected()
    {
        return donutCollected;
    }
}
