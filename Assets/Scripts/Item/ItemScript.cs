using UnityEngine;

public class ItemScript : MonoBehaviour
{
    [SerializeField] private float lifeTime = 15f;
    private void Start()
    {
        // Destroy the item after 15 seconds
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
