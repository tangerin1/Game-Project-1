using UnityEngine;

public class Flicker : MonoBehaviour
{
   public Light light;
   [SerializeField] private float minIntensity = 0f;
   [SerializeField] private float maxIntensity = 1f;
   [SerializeField] private float timeBetween = 0.5f;
   

    private float currentTimer;


    void Start()
    {
        light = GetComponent<Light>();
    }
    // Update is called once per frame
    void Update()
    {
        currentTimer  += Time.deltaTime;
        if (!(currentTimer >= timeBetween)) return;
        light.intensity = Random.Range(minIntensity, maxIntensity);
        currentTimer = 0;
    }
}
