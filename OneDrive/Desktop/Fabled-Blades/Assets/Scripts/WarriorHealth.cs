using UnityEngine;

public class WarriorHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    private Animator animator;
    private bool isDead = false;

    public HealthBar healthBar; // Reference to the heart-based UI

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

        // Null-safe lookup for HealthBar if not assigned in Inspector
        if (healthBar == null)
        {
            GameObject hb = GameObject.Find("HealthBar");
            if (hb != null)
            {
                healthBar = hb.GetComponent<HealthBar>();
            }
        }

        // ✅ Only use it if it's found
        if (healthBar != null)
        {
            healthBar.SetMaxHearts(maxHealth);
        }
        else
        {
            Debug.LogError("⚠️ HealthBar is not assigned and couldn't be found!");
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;

        if (healthBar != null)
        {
            healthBar.TakeDamage(amount);
        }

        Debug.Log("Warrior is hurt: " + currentHealth);
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }

        Camera.main.GetComponent<CameraShake>()?.Shake();
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Warrior is dead");

        animator.SetBool("isDead", true);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<WarriorController>().enabled = false;

        GameOverManager.TriggerGameOver();  // ✅ Show Game Over screen
    }

    public bool IsDead()
    {
        return isDead; // ✅ Just return the status
    }

    public void HealToFull()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.SetMaxHearts(maxHealth);
        }

        Debug.Log("Healed to full health!");
    }
}
