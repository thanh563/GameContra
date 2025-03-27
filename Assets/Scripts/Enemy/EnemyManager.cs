using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    public int health;
    private int currentHealth;
    public bool invinsible;

    public GameObject deathEffectFirst;
    public GameObject deathEffectSecond;

    // Use this for initialization
    void Start () {
        invinsible = false;
        currentHealth = health;
	}

    private void Update()
    {
        if (health <= 0 || gameObject.transform.position.y < -10f)
        {
            Die();
        }
    }

    // Умереть
    public void Die()
    {
        if (deathEffectFirst != null)
        {
            Instantiate(deathEffectFirst, transform.position, transform.rotation);
        }
        if (deathEffectSecond != null)
        {
            Instantiate(deathEffectSecond, transform.position, transform.rotation);
        }
        DropItemScript dropItemScript = gameObject.GetComponent<DropItemScript>();
        if (dropItemScript != null)
        {
            dropItemScript.DropItem();
        }
        Destroy(gameObject);
    }
}
