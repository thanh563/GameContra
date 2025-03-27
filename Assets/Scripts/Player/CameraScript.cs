using Unity.Cinemachine;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public CinemachineCamera virtualCamera;
    public Transform player;

    private float minX = -7.5f; // Dynamic left boundary
    private Transform cameraTransform;

    void Start()
    {
        if (virtualCamera != null)
        {
            cameraTransform = virtualCamera.transform;
        }
    }

    void LateUpdate()
    {
        if (player == null || cameraTransform == null) return;

        // Update minX only if the player moves further to the right
        if (player.position.x > minX)
        {
            minX = player.position.x;
        }

        // Restrict the camera from moving left beyond minX
        cameraTransform.position = new Vector3(
            Mathf.Max(cameraTransform.position.x, minX),
            cameraTransform.position.y,
            cameraTransform.position.z
        );
    }
}
