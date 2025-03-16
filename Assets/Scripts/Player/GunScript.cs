using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private Transform playerTransform;

    private List<GameObject> projectiles;
    private readonly int amoSize = 5;

    private void Start()
    {
        projectiles = new List<GameObject>();
        for (int i = 0; i < amoSize; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectiles.Add(projectile);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireProjectile();
        }
    }

    void FireProjectile()
    {
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePos.z = 0;
        //
        //Vector3 direction = (mousePos - transform.position).normalized;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set the projectile direction to move horizontally
        float facingDirection = Mathf.Sign(playerTransform.localScale.x);
        Vector3 direction = new(facingDirection, 0f, 0f);

        for (int i = 0; i < amoSize; i++)
        {
            BulletScript projectileScript = projectiles[i].GetComponent<BulletScript>();
            if (projectileScript.available)
            {
                projectiles[i].SetActive(true);
                projectiles[i].transform.position = firePoint.position;
                projectileScript.Launch(direction);
                break;
            }
        }
    }
}
