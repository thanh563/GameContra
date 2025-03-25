using System.Collections;
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
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject drone;
    [SerializeField] private float crouchHeight;
    [SerializeField] private Text moveSpeedUpgradeText;
    [SerializeField] private Text shieldUpgradeText;
    [SerializeField] private Text airGunUpgradeText;
    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;
    private Vector3 gunPosition;

    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private bool isGrounded;
    private bool isCrouching = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        originalColliderSize = boxCollider.size;
        originalColliderOffset = boxCollider.offset;
    }

    private void Update()
    {
        if (Time.timeScale == 0) return;
        // Movement Input
        float moveInput = Input.GetAxisRaw("Horizontal");
        
        // Ground Check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        animator.SetBool("IsFalling", !isGrounded);

        if (Input.GetKeyDown(KeyCode.S)) // Crouching
        {
            HandleCrouching();
        }

        // Flip sprite
        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1f, 1f) * 3.5f;
        }

        if(isCrouching) return; //cannot move while crouching

        HandleMovement(moveInput);
        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            HandleJump();
        }
        ApplyBetterJump();
    }
    private void HandleMovement(float moveInput)
    {
        Vector3 move = speed * Time.deltaTime * new Vector3(moveInput, 0f, 0f);
        transform.position += move;
        bool isMoving = !(Mathf.Abs(moveInput) == 0);
        animator.SetBool("IsRunning", isMoving);
    }

    private void HandleJump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
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
    void HandleCrouching()
    {
        gunPosition = gun.transform.position;

        ShieldScript shieldScript = shield.GetComponent<ShieldScript>();
        if (!isCrouching) // Crouching
        {
            animator.SetBool("IsCrouching", true);
            gun.transform.position = new Vector3(gunPosition.x, gunPosition.y - 0.17f, gunPosition.z);
            boxCollider.size = new Vector2(originalColliderSize.x, originalColliderSize.y * crouchHeight);
            boxCollider.offset = new Vector2(originalColliderOffset.x, originalColliderOffset.y - (originalColliderSize.y * (1 - crouchHeight) / 2));
            isCrouching = true;
            shieldScript.available = true;
        }
        else // Stand up
        {
            animator.SetBool("IsCrouching", false);
            gun.transform.position = new Vector3(gunPosition.x, gunPosition.y + 0.17f, gunPosition.z) ;
            boxCollider.size = originalColliderSize;
            boxCollider.offset = originalColliderOffset;
            isCrouching = false;
            shieldScript.available = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            float dmg = 20f;
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                dmg = enemy.damage;
            }
            TakeDamage(dmg);    
        }
        //else if (collision.gameObject.CompareTag("EnemyBullet") && collision.gameObject != shield)
        //{
        //    float dmg = 20f;
        //    EnemyBulletScript bullet = collision.gameObject.GetComponent<EnemyBulletScript>();
        //    if (bullet != null)
        //    {
        //        dmg = bullet.currentDamage;
        //    }
        //    TakeDamage(dmg); // Adjust damage value as needed
        //}
        else if (collision.gameObject.CompareTag("Drone"))
        {
            TakeDamage(20);
        }
        else if (collision.gameObject.CompareTag("Heal"))
        {
            health += 10;
            hpFill.fillAmount = health / 100;
        }
        else if (collision.gameObject.CompareTag("MoveSpeedUpgrade"))
        {
            speed += 0.1f * speed;
            moveSpeedUpgradeText.text = int.Parse(moveSpeedUpgradeText.text) + 1 + "";
        }
        else if (collision.gameObject.CompareTag("AirGunUpgrade"))
        {
            speed += 0.1f * speed;
            airGunUpgradeText.text = int.Parse(airGunUpgradeText.text) + 1 + "";
            drone.GetComponent<DroneGunScript>().upgrade = true;
        }
        else if (collision.gameObject.CompareTag("ShieldUpgrade"))
        {
            shieldUpgradeText.text = int.Parse(shieldUpgradeText.text) + 1 + "";
        }
    }

    private void TakeDamage(float damage)
    {
        health -= 10;
        hpFill.fillAmount = health / 100;
        if (health <= 0)
        {
            // trigger death animation
            animator.SetTrigger("Death");
            this.enabled = false;
            StartCoroutine(WaitForDeathAnimation());
        }
    }

    private IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Time.timeScale = 0;
    }
}
