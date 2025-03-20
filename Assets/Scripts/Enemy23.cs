using UnityEngine;
using UnityEngine.UI;

public class Enemy23 : MonoBehaviour
{
	// Shooting
	public bool isShootable = false;
	public GameObject bullet;
	public float bulletSpeed;
	public float timeBtwFire; // Bắn mỗi lần cách nhau bao nhiêu giây
	private float fireCooldown;

	// Health
	[SerializeField] protected float maxHp = 50f;
	protected float currentHealth;
	[SerializeField] private Image hpBar;

	// Start is called before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		currentHealth = maxHp;
		UpdateHpBar();
		fireCooldown = timeBtwFire;
	}

	// Update is called once per frame
	void Update()
	{
		GameObject player = GameObject.Find("Player");
		if (player != null)
		{
			FlipTowardsPlayer(player.transform.position);
		}

		fireCooldown -= Time.deltaTime;
		if (fireCooldown <= 0)
		{
			fireCooldown = timeBtwFire;
			EnemyFireBullet();
		}
	}

	void EnemyFireBullet()
	{
		var bulletTmp = Instantiate(bullet, transform.position, Quaternion.identity);
		Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
		GameObject player = GameObject.Find("Player");

		if (player != null)
		{
			Vector3 direction = player.transform.position - transform.position;
			rb.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);
		}
	}

	void FlipTowardsPlayer(Vector3 playerPosition)
	{
		if (playerPosition.x > transform.position.x)
		{
			transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
		}
		else
		{
			transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
		}
	}

	public void TakeDamage(float damage)
	{
		currentHealth -= damage;
		currentHealth = Mathf.Max(currentHealth, 0);
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
