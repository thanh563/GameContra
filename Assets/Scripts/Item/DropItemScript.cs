using UnityEngine;

public class DropItemScript : MonoBehaviour
{
    [System.Serializable]
    public struct Item
    {
        public GameObject item;
        public float chance;
    }

    [SerializeField] private Item[] items;

    public void DropItem()
    {
        foreach (Item item in items)
        {
            if (Random.value <= item.chance)
            {
                Drop(item.item);
            }
        }
    }

    private void Drop(GameObject itemPrefab)
    {
        GameObject spawnedItem = Instantiate(itemPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = spawnedItem.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector2 forceDirection = new Vector2(Random.Range(-1f, 1f), 1f);
            float forceStrength = 5f;
            rb.AddForce(forceDirection * forceStrength, ForceMode2D.Impulse);
        }
    }
}
