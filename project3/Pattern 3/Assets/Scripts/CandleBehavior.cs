using UnityEngine;

public class CandleBehavior : MonoBehaviour
{
    public ParticleSystem flame;
    public ParticleSystemRenderer flameRenderer;
    public Light flameLight;
    [ColorUsage(true, true)]
    public Color flameColor = Color.lightGoldenRodYellow;
    public bool flameEnabled = true;
    private Material flameMaterial;
    public Renderer candleRenderer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flameRenderer = flame.GetComponent<ParticleSystemRenderer>();
        flameMaterial = flameRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void ToggleFlame()
    {
        flameEnabled = !flameEnabled;
        flame.gameObject.SetActive(flameEnabled);
    }

    void ChangeColor(Color color)
    {
        flameColor = color;
        var main = flame.main;
        main.startColor = color;
        flameMaterial.SetColor("_Color", color * 2f);
        Color.RGBToHSV(color, out float h, out float s, out float v);
        flameLight.color = Color.HSVToRGB(h, s, 1f);
        Material candleMaterial = candleRenderer.material;
        candleMaterial.SetColor("_EmissionColor", color * 1.5f);
        
    }
}
