using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    [Header("Health Bar Settings")]
    public float maxHealth = 200f;
    public float currentHealth = 200f;

    [Header("UI Elements")]

    public UnityEngine.UI.Image healthBarFill;
    public RectTransform notch;
    // Start is called before the first frame 
    // update
    void Start()
    {
        UpdateHealthBar();
        PositionNotch();
    }

    public void TakeDamage(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        float amount = currentHealth / maxHealth;
        healthBarFill.fillAmount = amount;
    }

    void PositionNotch()
    {
        float half = 0.5f;

        float width = healthBarFill.rectTransform.rect.width;

        // move notch
        notch.anchoredPosition = new UnityEngine.Vector2(width * half, 0f);
    }
}
