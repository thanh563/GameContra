using UnityEngine;

public class LootItemsScript : MonoBehaviour
{
    [SerializeField] private GameObject upgrade;
    [SerializeField] private GameObject heal;

    public void DropUpgrade()
    {
        //float chance = Random.value;

        //if (chance < 0.09f) 
        //{
            Instantiate(upgrade, transform.position, Quaternion.identity);
            Rigidbody2D rb = upgrade.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 forceDirection = new Vector2(Random.Range(-1f, 1f), 1f);
                float forceStrength = 20f; 
                rb.AddForce(forceDirection * forceStrength, ForceMode2D.Impulse);
            }
        //}
    }

    public void DropHeal()
    {
        //float chance = Random.value;

        //if (chance < 0.2f)
        //{
            Instantiate(heal, transform.position, Quaternion.identity);
            Rigidbody2D rb = heal.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 forceDirection = new Vector2(Random.Range(-1f, 1f), 1f);
                float forceStrength = 20f;
                rb.AddForce(forceDirection * forceStrength, ForceMode2D.Impulse);
            }
        //}
    }
    public void DropAllLoot()
    {
        float chance = Random.value;
        if (chance <= 0.4f) DropUpgrade();
        else DropHeal();
    }
}
