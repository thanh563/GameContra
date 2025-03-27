using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {

    Rigidbody2D myRigidbody;
    public float movespeed;
    public float lifeTime = 3f;
    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myRigidbody.AddRelativeForce(Vector2.right * (movespeed + PlayerMovement.rapidsPicked * PlayerMovement.projectileSpeedKoeff), ForceMode2D.Impulse);
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
