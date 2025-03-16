using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float fallMultiplier = 2.5f;  // Faster falling
    [SerializeField] private float lowJumpMultiplier = 2f; // Shorter jump if button is released early
    [SerializeField] private float health = 100;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Image hpFill;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Time.timeScale == 0) return;

        // Movement
        float moveInput = Input.GetAxisRaw("Horizontal");
        Vector3 move = speed * Time.deltaTime * new Vector3(moveInput, 0f, 0f);
        transform.position += move;

        // Animation
        bool isMoving = !(Mathf.Abs(moveInput) == 0);
        animator.SetBool("IsRunning", isMoving);

        // Flip sprite
        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1f, 1f);
        }

        // Ground Check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        animator.SetBool("IsFalling", !isGrounded);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        ApplyBetterJump();

        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    // trigger death animation
        //                animator.SetTrigger("Death");
        //                this.enabled = false;
        //}
    }

    private void ApplyBetterJump()
    {
        // If falling (velocity.y < 0), apply stronger gravity for a faster fall
        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = fallMultiplier;
        }
        // If going up but the player released the jump button, apply lowJumpMultiplier
        else if (rb.linearVelocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.gravityScale = lowJumpMultiplier;
        }
        else
        {
            rb.gravityScale = 1f; // Reset gravity when grounded or moving up normally
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health -= 10;
            hpFill.fillAmount = health / 100;
            if (health <= 0)
            {
                // trigger death animation
                animator.SetTrigger("Death");
                this.enabled = false;
            }
        }
    }
}
