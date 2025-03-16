using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private GameObject player;

    private List<GameObject> bullets;
    private readonly int amoSize = 15;

    public int currentUpgrade = 0;
    private float bulletDamage = 0;
    private void Start()
    {
        bulletDamage = bulletPrefab.GetComponent<BulletScript>().baseDamage;

        bullets = new List<GameObject>();
        for (int i = 0; i < amoSize; i++)
        {
            GameObject projectile = Instantiate(bulletPrefab);
            bullets.Add(projectile);
        }
    }

    public void UpgradeGun()
    {
        foreach (GameObject bullet in bullets)
        {
            bullet.GetComponent<BulletScript>().currentDamage += currentUpgrade * 10f;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentUpgrade == 0 || currentUpgrade == 1)
            {
                FireProjectile0();
            }
            else if (currentUpgrade == 2)
            {
                FireProjectile1();
            }
        }
    }

    void FireProjectile0()
    {
        float facingDirection = Mathf.Sign(player.transform.localScale.x);
        Vector3 direction = new(facingDirection, 0f, 0f);

        for (int i = 0; i < amoSize; i++)
        {
            BulletScript bulletscript = bullets[i].GetComponent<BulletScript>();
            if (bulletscript.available)
            {
                bullets[i].SetActive(true);
                bullets[i].transform.position = firePoint.position;
                bulletscript.Launch(direction);
                break;
            }
        }
    }
    void FireProjectile1()
    {
        float facingDirection = Mathf.Sign(player.transform.localScale.x);
        Vector3 baseDirection = new(facingDirection, 0f, 0f);
        float spreadAngle = 15f * Mathf.Deg2Rad; // Convert degrees to radians

        int bulletsFired = 0; // Track how many bullets we fire

        for (int i = 0; i < amoSize; i++)
        {
            BulletScript bulletscript = bullets[i].GetComponent<BulletScript>();
            if (bulletscript.available)
            {
                bullets[i].SetActive(true);
                bullets[i].transform.position = firePoint.position;

                // Modify direction based on bullets fired (middle, upper, lower)
                Vector3 direction = baseDirection;
                if (bulletsFired == 1) // Upper bullet
                    direction = new Vector3(Mathf.Cos(spreadAngle) * facingDirection, Mathf.Sin(spreadAngle), 0f);
                else if (bulletsFired == 2) // Lower bullet
                    direction = new Vector3(Mathf.Cos(-spreadAngle) * facingDirection, Mathf.Sin(-spreadAngle), 0f);

                bulletscript.Launch(direction);
                bulletsFired++;

                if (bulletsFired >= 3) // Only fire 3 bullets
                    break;
            }
        }
    }

}
