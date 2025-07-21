using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed = 2f;
    public float chaseRange = 5f;
    public float attackRange = 1f;
    public float attackRate = 1f;
    public int attackDamage = 1;

    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float nextAttackTime = 0f;

    private WarriorHealth warrior;
    private bool hasStoppedAfterDeath = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (target != null)
            warrior = target.GetComponent<WarriorHealth>();
    }

    void Update()
    {
        if (target == null || warrior == null)
        {
            animator.SetBool("isRunning", false);
            return;
        }

        if (warrior.IsDead())
        {
            if (!hasStoppedAfterDeath)
            {
                animator.SetBool("isRunning", false);
                rb.velocity = Vector2.zero;
                hasStoppedAfterDeath = true;
            }
            return;
        }

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance <= chaseRange && distance > attackRange)
        {
            animator.SetBool("isRunning", true);
            Vector2 direction = (target.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            if (direction.x != 0)
                spriteRenderer.flipX = (direction.x < 0);
        }
        else if (distance <= attackRange)
        {
            animator.SetBool("isRunning", false);

            if (Time.time >= nextAttackTime)
            {
                animator.SetTrigger("attack");
                nextAttackTime = Time.time + 1f / attackRate;

                warrior.TakeDamage(attackDamage);
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        if (warrior.IsDead())
        {
            if (!hasStoppedAfterDeath)
            {
                animator.ResetTrigger("attack"); // <- Cancel attack animation
                animator.SetBool("isRunning", false);
                rb.velocity = Vector2.zero;
                hasStoppedAfterDeath = true;
            }
            return;
        }

    }
}
