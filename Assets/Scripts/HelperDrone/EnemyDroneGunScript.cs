using UnityEngine;

public class EnemyDroneGunScript : MonoBehaviour
{
    public GameObject target;
    [SerializeField]
    private GameObject bulletPrefab;
    public float fireInterval = 2f; // Fire every 2 seconds
    private float nextFireTime;

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireInterval;
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && target != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            
            if (bullet.TryGetComponent<EnemyDroneBulletScript>(out var bulletScript))
            {
                Vector3 direction = (target.transform.position - transform.position).normalized;
                bulletScript.SetDirection(new Vector3(direction.x, direction.y-1, direction.z));
            }
        }
    }
}
