using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    public float movespeed;
    public float lifeTime = 5f;
    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        var speed = Random.Range(-4, 4) + movespeed;

        myRigidbody.AddRelativeForce(Vector2.left * (speed + PlayerMovement.rapidsPicked * PlayerMovement.projectileSpeedKoeff), ForceMode2D.Impulse);//playerControleer
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
