using UnityEngine;

public class HelperDroneScript : MonoBehaviour
{
    public bool available;

    [SerializeField] private float speed = 3f;
    [SerializeField] private float offScreenDuration = 3f; // Time before deactivating

    public bool movingRight;
    private DropItemScript lootScript;
    private float offScreenTimer = 0f;
    private bool isOffScreen = false;

    void Start()
    {
        lootScript = GetComponent<DropItemScript>();
        gameObject.SetActive(false);
    }

    void Update()
    {
        // Move left or right
        float move = speed * Time.deltaTime * (movingRight ? 1 : -1);
        transform.Translate(move, 0, 0);

        CheckOffScreen(); // Ensure drones deactivate when off-screen
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            lootScript.DropItem();
            gameObject.SetActive(false);
        }
    }

    private void CheckOffScreen()
    {
        Vector3 screenPosition = Camera.main.WorldToViewportPoint(transform.position);

        if (screenPosition.x < -0.1f || screenPosition.x > 1.1f) // Outside screen bounds
        {
            if (!isOffScreen)
            {
                isOffScreen = true;
                offScreenTimer = 0f;
            }

            offScreenTimer += Time.deltaTime;

            if (offScreenTimer >= offScreenDuration)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            isOffScreen = false;
            offScreenTimer = 0f; // Reset timer when visible
        }
    }

    private void OnDisable()
    {
        available = true;
    }

    private void OnEnable()
    {
        available = false;
    }
}
