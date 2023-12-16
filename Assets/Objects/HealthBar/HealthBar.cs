using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;
    
    public void SetHealth(float health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;

        fill.color = gradient.Evaluate(1f);
    }

    public void ChangeMaxHealthAccordingItems(float newMax)
    {
        float oldMax = slider.maxValue;
        float oldValue = slider.value;
        float oldRatio = oldValue / oldMax;
        float newValue = oldRatio * newMax;

        slider.maxValue = newMax;
        slider.value = newValue;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
