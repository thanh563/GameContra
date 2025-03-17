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
    private void Start()
    {
        bullets = new List<GameObject>();
        for (int i = 0; i < amoSize; i++)
        {
            GameObject projectile = Instantiate(bulletPrefab);
            bullets.Add(projectile);
        }
    }

    public void UpgradeGun()
    {
        float scale = currentUpgrade * 0.01f;
        foreach (GameObject bullet in bullets)
        {
            bullet.GetComponent<BulletScript>().currentDamage += currentUpgrade * 10f;
            bullet.transform.localScale += new Vector3(scale, scale, scale);
        }
        
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (currentUpgrade == 0 || currentUpgrade == 1)
            {
                FireProjectile(0);
            }
            else if (currentUpgrade == 2 || currentUpgrade == 3)
            {
                FireProjectile(1);
            }
        }
    }

    void FireProjectile(int mode)
    {
        float facingDirection = Mathf.Sign(player.transform.localScale.x);
        Vector3 baseDirection = new(facingDirection, 0f, 0f);
        float spreadAngle = 10f * Mathf.Deg2Rad; // Convert degrees to radians

        int bulletsFired = 0; // Track how many bullets we fire

        for (int i = 0; i < amoSize; i++)
        {
            BulletScript bulletscript = bullets[i].GetComponent<BulletScript>();
            if (bulletscript.available)
            {
                bullets[i].SetActive(true);
                bullets[i].transform.position = firePoint.position;

                // Determine bullet direction
                Vector3 direction = baseDirection;
                if (mode == 1) // Spread Shot
                {
                    if (bulletsFired == 1) // Upper bullet
                        direction = new Vector3(Mathf.Cos(spreadAngle) * facingDirection, Mathf.Sin(spreadAngle), 0f);
                    else if (bulletsFired == 2) // Lower bullet
                        direction = new Vector3(Mathf.Cos(-spreadAngle) * facingDirection, Mathf.Sin(-spreadAngle), 0f);
                }

                bulletscript.Launch(direction);
                bulletsFired++;

                if ((mode == 0 && bulletsFired >= 1) || (mode == 1 && bulletsFired >= 3))
                    break;
            }
        }
    }
}
