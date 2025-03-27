using UnityEngine;
using System.Collections;

public class KillPlayerOnContact : MonoBehaviour {

    public bool killSelf;
    public bool KillRegardless;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //if(KillRegardless) FindObjectOfType<PlayerMovement>().invincCounter = -1;
            //FindObjectOfType<PlayerMovement>().Death();
            if (killSelf) Destroy(gameObject);
        }

    }
}
