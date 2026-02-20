
using UnityEngine;
using TMPro;

public class Donut : MonoBehaviour
{
    
    public static bool donutCollected = false;
    public TMP_Text text;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        donutCollected = false;
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
            text.enabled = false;
        }
    }

    public bool getDonutCollected()
    {
        return donutCollected;
    }
}
