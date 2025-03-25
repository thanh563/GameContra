using UnityEngine;

public class DroneScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject gameController;
    private Vector3 targetOffset;
    private bool isMainWeapon = false;
    private float switchThreshold = 0.5f; // Distance before the drone starts following
    private float followSpeed = 7f; // Speed at which the drone moves

    void Start()
    {
        if (player != null)
        {
            targetOffset = transform.position - player.transform.position;
        }
    }

    void Update()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 dronePos = transform.position;
        float playerDirection = player.GetComponent<Rigidbody2D>().linearVelocity.x; // Assuming 2D physics
        transform.localScale = new Vector3(Mathf.Sign(player.GetComponent<Rigidbody2D>().linearVelocity.x)*0.8f, 0.8f, 0.8f);

        // Check if the player has moved past the drone
        if (Mathf.Abs(playerPos.x - dronePos.x) > switchThreshold)
        {
            if (playerDirection > 0)
                targetOffset = new Vector3(-Mathf.Abs(targetOffset.x), targetOffset.y, targetOffset.z); // Move behind when going right
            else if (playerDirection < 0)
                targetOffset = new Vector3(Mathf.Abs(targetOffset.x), targetOffset.y, targetOffset.z); // Move behind when going left
        }

        // Smoothly move toward the target position
        transform.position = Vector3.Lerp(transform.position, playerPos + targetOffset, followSpeed * Time.deltaTime);

        // Weapon switching
        if (Input.GetMouseButtonDown(1))
        {
            int weapon = isMainWeapon ? 2 : 1;
            GameControllerScript gameControllerScript = gameController.GetComponent<GameControllerScript>();
            gameControllerScript.SwitchWeapon(weapon);
            SwitchWeapon();
        }

        if (!isMainWeapon) return;

        // Rotate toward mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector3 direction = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void SwitchWeapon()
    {
        isMainWeapon = !isMainWeapon;
    }
}
