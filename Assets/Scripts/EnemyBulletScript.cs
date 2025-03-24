using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public Vector3 direction;

    [SerializeField]
    private float bulletSpeed;
    private Rigidbody2D rigidbody;
    private float timer = 3f;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (direction == null) return;
        if (rigidbody == null) return;
        rigidbody.linearVelocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);    
        }
    }

    private void OnBecameInvisible()
    {
        Invoke(nameof(DestroyBullet), timer);
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

}
