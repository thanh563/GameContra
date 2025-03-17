using UnityEngine;
using UnityEngine.UI;
public class UpgradeScript : MonoBehaviour
{
    public int currentUpgrade = 0;
    //[SerializeField] GameObject player;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject gun;
    [SerializeField] private Text upgradeText;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Upgrade"))
        {
            if (currentUpgrade < 3)
            {
                currentUpgrade++;

                //change gun firing mode 
                GunScript gs = gun.GetComponent<GunScript>();
                gs.currentUpgrade = currentUpgrade;
                gs.UpgradeGun();
                upgradeText.text = currentUpgrade + 1 + "";
            }
        }
    }
}
