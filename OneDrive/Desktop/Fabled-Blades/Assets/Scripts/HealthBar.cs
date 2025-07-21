using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public List<Image> hearts; // Assign heart Images in order in Inspector

    private int currentHealth;

    public void SetMaxHearts(int maxHealth)
    {
        currentHealth = maxHealth;

        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < maxHealth)
                hearts[i].enabled = true; // Show full hearts
            else
                hearts[i].enabled = false; // Hide extra hearts
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);

        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < currentHealth)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false; // Just turn off the hearts
        }
    }
}
