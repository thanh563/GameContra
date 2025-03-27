using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 moveDirection;
    public float baseDamage = 10f;
    public float currentDamage = 10f;
    public bool available;
    public AudioSource aus; 
    public AudioClip hitSound; 

    public void Start()
    {
        gameObject.SetActive(false);
    }
    public void Launch(Vector3 direction)
    {
        moveDirection = direction;
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        if (available) return;
        transform.position += speed * Time.deltaTime * moveDirection;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Drone"))
        {

            gameObject.SetActive(false);
        }
    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        available = true;
    }

    private void OnEnable()
    {
        available = false;
    }
}
