using UnityEngine;

public class CandleBehavior : MonoBehaviour
{
    public ParticleSystem flame;
    public ParticleSystemRenderer flameRenderer;
    [ColorUsage(true, true)]
    public Color flameColor = Color.lightGoldenRodYellow;
    public bool flameEnabled = true;
    private Material flameMaterial;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flameRenderer = flame.GetComponent<ParticleSystemRenderer>();
        flameMaterial = flameRenderer.material;
        ChangeColor(flameColor);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleFlame();
        }

        if (Input.GetKeyDown(KeyCode.K)) 
        {
            ChangeColor(Color.blue);
        }
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
        flameMaterial.SetColor("_Color", color);
        
    }
}
