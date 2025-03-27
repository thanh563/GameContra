using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class AngleChecker : MonoBehaviour {

    private EnemyRunner player; //thay PlayerController thanh EnemyRunner

	private float angle;
    private Vector2 A, B, C;

    void Start()
    {
        player = FindObjectOfType<EnemyRunner>(); //    thay PlayerController thanh EnemyRunner

	}

    public float checkAngle()
    {
        A = new Vector2(transform.position.x, transform.position.y);
        B = new Vector2(player.transform.position.x, player.transform.position.y);
        C = B - A;

        angle = Mathf.Atan2(C.y, C.x) * Mathf.Rad2Deg;
        angle = Mathf.Round(angle / 30) * 30;

        return angle;
    }

}
