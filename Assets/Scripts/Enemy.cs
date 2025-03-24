using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private GameObject target;
    [SerializeField] private float distance = 5f;
    private Vector3 startPos;
    private bool movingRight = true;

    // Shooting
    public float damage = 20f;
    public bool isShootable = false;
    public GameObject bullet;
    public float bulletSpeed;
    public float timeBtwFire;
    private float fireCooldown;

    // Health
    [SerializeField] private float maxHp = 50f;
    private float currentHealth;
    [SerializeField] private Image hpBar;

    void Start()
    {
        startPos = transform.position;
        currentHealth = maxHp; // Set HP first
        UpdateHpBar(); // Then update HP bar
    }

    void Update()
    {
        float leftBound = startPos.x - distance;
        float rightBound = startPos.x + distance;

        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x >= rightBound)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x <= leftBound)
            {
                movingRight = true;
                Flip();
            }
        }

        fireCooldown -= Time.deltaTime;
        if (fireCooldown < 0 && isShootable)
        {
            fireCooldown = timeBtwFire;
            EnemyFireBullet();
        }
    }

    void EnemyFireBullet()
    {
        if (bullet == null) return; // Prevents errors
        if (target == null) return;
        var bulletTmp = Instantiate(bullet, transform.position, Quaternion.identity);
        EnemyBulletScript enemyBulletScript = bulletTmp.GetComponent<EnemyBulletScript>();

        Vector3 direction = target.transform.position - transform.position;
        direction.y = 0;
        direction.Normalize();

        enemyBulletScript.direction = direction;
    }

    void Flip()
    {
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            float dmg = collision.gameObject.GetComponent<BulletScript>().currentDamage;
            TakeDamage(dmg);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        if (hpBar != null) UpdateHpBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void UpdateHpBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = currentHealth / maxHp;
        }
    }
}
