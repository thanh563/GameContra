using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [Header("Di chuyển")]
    public float moveSpeed = 5f;   // Tốc độ di chuyển
    public float jumpForce = 10f;  // Lực nhảy

    [Header("Kiểm tra mặt đất")]
    public LayerMask groundLayer;  // Lớp mặt đất
    private bool isGrounded;       // Kiểm tra có đang đứng trên đất không

    private Rigidbody2D rb;
    private BoxCollider2D coll;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();

        if (rb == null)
        {
            Debug.LogError("Thiếu Rigidbody2D! Hãy thêm Rigidbody2D vào Player.");
        }

        if (coll == null)
        {
            Debug.LogError("Thiếu BoxCollider2D! Hãy thêm BoxCollider2D vào Player.");
        }
    }

    void Update()
    {
        Move();
        CheckGround();
        Jump();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal"); // A/D hoặc ⬅️ ➡️
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Lật Player theo hướng di chuyển
        if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void CheckGround()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, coll.bounds.extents.y + 0.1f, groundLayer);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded) // Nhấn Space để nhảy
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
}
