using UnityEngine;

public class WarriorController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded = true;
    private SpriteRenderer spriteRenderer;

    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayers;
    public int attackDamage = 1;

    public bool isFrozen = false; // NEW: Control input freeze
    public AudioSource audioSource;
    public AudioClip jumpClip;
    public AudioClip swordClip;
    private PlayerAudio playerAudio;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAudio = GetComponent<PlayerAudio>();
    }

    void Update()
    {
        if (isFrozen)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            animator.SetFloat("Speed", 0);
            return; // skip movement and attack
        }

        // Move
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(moveInput));

        // Flip sprite
        if (moveInput > 0)
            spriteRenderer.flipX = false;
        else if (moveInput < 0)
            spriteRenderer.flipX = true;

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("IsJumping", true);
            isGrounded = false;
            playerAudio.PlayJumpSound();
        }

        // Attack
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("Attack");
            playerAudio.PlaySwordSound();

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyController>()?.TakeDamage(attackDamage);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("IsJumping", false);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
