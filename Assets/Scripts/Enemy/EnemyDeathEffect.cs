using UnityEngine;
using System.Collections;

public class EnemyDeathEffect : MonoBehaviour {

    public float height;
    private int scale;

	// Use this for initialization
	void Start () {
        scale = Mathf.Clamp((int)(FindObjectOfType<PlayerMovement>().transform.position.x*10000 - transform.position.x * 10000), -1, 1);//thay PlayerController thanh EnemyRunner
		transform.localScale = new Vector3(scale, 1, 1);
        if(GetComponent<Rigidbody2D>() != null)
        {
            GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-scale*3, height);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
