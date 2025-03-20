using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float spped = 2f;
    [SerializeField] private float distance = 5f;
    private Vector3 startPos;
    private bool movingRight = true;
	//Shooting
	public bool isShootable = false;
	public GameObject bullet;
    public float buttletSpeed;
    public float timeBtwFire;// bnh giây bắn 1 lần
    public float fireCooldown;
    //Health
    [SerializeField]protected float maxHp=50f;
	protected float currentHealth;
    [SerializeField] private Image hpBar;


	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        startPos = transform.position;
		UpdateHpBar();
		currentHealth = maxHp;
	}

    // Update is called once per frame
    void Update()
    {
        float leftBound = startPos.x - distance;
        float rightBound = startPos.x + distance;
        if (movingRight)
        {
            transform.Translate(Vector2.right * spped * Time.deltaTime);
            if (transform.position.x >= rightBound)
            {
                movingRight = false;
				Flip();
			}
        }
        else
        {
            transform.Translate(Vector2.left * spped * Time.deltaTime);
            if (transform.position.x <= leftBound)
            {
                movingRight = true;
                Flip();
            }
        }
		fireCooldown -= Time.deltaTime;
        if(fireCooldown < 0)
        {
            fireCooldown = timeBtwFire;
            //shoot
            EnemyFireBullet();
        }
	}
    void EnemyFireBullet()
    {
        var bulletTmp = Instantiate(bullet, transform.position, Quaternion.identity);

		Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
        Vector3 direction = GameObject.Find("Player").transform.position - transform.position;
        rb.AddForce(direction.normalized * buttletSpeed, ForceMode2D.Impulse);
	}
    void Flip()
    {
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
	}
    public void TakeDamage(float damage)
	{
		currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
		hpBar.fillAmount = currentHealth / maxHp;
        UpdateHpBar();
		if (currentHealth <= 0)
		{
            Die();
		}
	}
	public void Die()
    {
		Destroy(gameObject);
	}
    protected void UpdateHpBar()
    {
        if (hpBar != null)
        {
			hpBar.fillAmount = currentHealth / maxHp;
		}
    }
}