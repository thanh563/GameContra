using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    int currentHealth;
    public HealthBar healthBar;
    public UnityEvent OnDeath;

	public void OnEnable()
	{
		OnDeath.AddListener(Death);
	}
	public void OnDisable()
	{
		OnDeath.RemoveListener(Death);
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateBar(currentHealth, maxHealth);
    }
    public void TakeDamege(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
			currentHealth = 0;
			OnDeath.Invoke();
		}
        healthBar.UpdateBar(currentHealth, maxHealth);
	}
    public void Death()
    {
        Destroy(gameObject);
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamege(20);
        }
    }
}
