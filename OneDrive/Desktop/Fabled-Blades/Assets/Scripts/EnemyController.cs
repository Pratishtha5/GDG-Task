using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // Prevent further damage

        currentHealth -= damage;
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");

        // Stop AI movement if using EnemyAI script
        EnemyAI ai = GetComponent<EnemyAI>();
        if (ai != null) ai.enabled = false;

        // Disable Collider and Physics
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        // Optional: Play death sound or particle

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerStats>()?.GainXP(20);
        }

        Destroy(gameObject);
    }

}
