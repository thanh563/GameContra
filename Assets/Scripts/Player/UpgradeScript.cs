using UnityEngine;

public class UpgradeScript : MonoBehaviour
{
    public int currentUpgrade = 0;
    //[SerializeField] GameObject player;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject gun;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Upgrade"))
        {
            if (currentUpgrade < 2)
            {
                currentUpgrade++;

                //change gun firing mode 
                GunScript gs = gun.GetComponent<GunScript>();
                gs.currentUpgrade = currentUpgrade;
                gs.UpgradeGun();
            }
        }
    }
}
