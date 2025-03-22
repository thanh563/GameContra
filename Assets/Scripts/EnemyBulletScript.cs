using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    private float timer = 3f;
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
