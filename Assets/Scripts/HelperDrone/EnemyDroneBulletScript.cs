using UnityEngine;

public class EnemyDroneBulletScript : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 direction;
    private float lifeTime = 3f;
    private float baseDamage = 20f;
    public float currentDamage;

    private void Start()
    {
        currentDamage = baseDamage;
        Destroy(gameObject, lifeTime);
    }
    // Function to set the direction of the bullet
    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized; // Normalize to maintain consistent speed
    }

    void Update()
    {
        transform.position += speed * Time.deltaTime * direction; // Move bullet in the given direction
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Shield"))
        {
            Destroy(gameObject);
        }
    }
}
