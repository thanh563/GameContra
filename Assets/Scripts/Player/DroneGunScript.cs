using System.Collections.Generic;
using UnityEngine;

public class DroneGunScript : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private readonly int amoSize = 5;

    private bool isMainWeapon = false;

    private List<GameObject> bullets;
    private void Start()
    {
        bullets = new List<GameObject>();
        for (int i = 0; i < amoSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullets.Add(bullet);
        }
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        if (Input.GetMouseButtonDown(1)) {
            SwitchWeapon();
        }

        if (Input.GetMouseButtonDown(0) && isMainWeapon)
        {
            Fire();
        }
    }



    void Fire()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector3 direction = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        for (int i = 0; i < amoSize; i++)
        {
            BulletScript bs = bullets[i].GetComponent<BulletScript>();
            if (bs.available)
            {
                bullets[i].SetActive(true);
                bullets[i].transform.position = firePoint.position;
                bs.Launch(direction);
                break;
            }
        }
    }

    private void SwitchWeapon()
    {
        isMainWeapon = !isMainWeapon;
    }
}
