using UnityEngine;

public class CandleBehavior : MonoBehaviour
{
    public ParticleSystem flame;
    public ParticleSystemRenderer flameRenderer;
    [ColorUsage(true, true)]
    public Color flameColor = Color.yellow;
    public bool flameEnabled = true;
    private Material flameMaterial;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flame = GetComponent<ParticleSystem>();
        flameRenderer = flame.GetComponent<ParticleSystemRenderer>();
        flameColor = Color.yellow;
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
        if (flameEnabled)
        {
            flame.gameObject.SetActive(false);
        }

        else
        {
            flame.gameObject.SetActive(true);
        }
    }

    void ChangeColor(Color color)
    {
        flameColor = color;
        flameMaterial.SetColor("_Color", color);
    }
}
