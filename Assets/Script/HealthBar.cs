using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider; // Reference to the UI Slider component
    public Gradient gradient; // Optional: Gradient for health bar color
    public Image fill; // Reference to the UI Image component for fill color

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        fill.color = gradient.Evaluate(1f); // Set initial fill color
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue); // Update fill color based on health percentage
    }
}