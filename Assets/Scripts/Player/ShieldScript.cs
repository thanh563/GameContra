using UnityEngine;
using System.Collections;

public class ShieldScript : MonoBehaviour
{
    public float maxHealth = 100;
    [SerializeField]
    private float currentHealth;
    private Collider2D shieldCollider;
    private SpriteRenderer shieldRenderer;
    [SerializeField]
    private bool isRegenerating = false;
    public bool available = false;

    private void Update()
    {
        if (available && !isRegenerating)
        {
            ActivateShield();
        }
        else if (!available)
        {
            DeactivateShield();
        }
    }

    private void Awake()
    {
        shieldCollider = GetComponent<Collider2D>();
        shieldRenderer = GetComponent<SpriteRenderer>();

        currentHealth = maxHealth; // Set shield health to max at start
        DeactivateShield();
    }

    public void ActivateShield()
    {
        if (!isRegenerating && !shieldCollider.enabled) // Prevent resetting if already active
        {
            shieldCollider.enabled = true;
            if (shieldRenderer != null) shieldRenderer.enabled = true;
            currentHealth = maxHealth; // Reset health only when reactivating
        }
    }

    public void DeactivateShield()
    {
        shieldCollider.enabled = false;
        if (shieldRenderer != null) shieldRenderer.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            float dmg = enemy != null ? enemy.damage : 20; // Use ternary to check for enemy damage
            TakeDamage(dmg);
        }
        // else if (collision.gameObject.CompareTag("EnemyBullet"))
        // {
        //     Debug.Log("Shield hit");
        //     EnemyBulletScript bullet = collision.gameObject.GetComponent<EnemyBulletScript>();
        //     float dmg = bullet != null ? bullet.currentDamage : 20;
        //     TakeDamage(dmg);
        // }
    }

    private void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 && !isRegenerating)
        {
            StartCoroutine(RegenerateShield());
        }
    }

    private IEnumerator RegenerateShield()
    {
        isRegenerating = true;
        DeactivateShield();
        yield return new WaitForSeconds(5f); // Wait 5 seconds before reactivating
        isRegenerating = false;
        ActivateShield();
    }
}