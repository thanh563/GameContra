using UnityEngine;

public class MainCameraFollow : MonoBehaviour
{
    public Transform player;  // Player c?n theo d�i
    public float smoothSpeed = 5f; // T?c ?? m??t m�

    private Vector3 offset; // ?? l?ch gi?a Player v� Camera

    void Start()
    {
        if (player != null)
        {
            // L?y kho?ng c�ch ban ??u gi?a Camera v� Player
            offset = transform.position - player.position;
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // T�nh to�n v? tr� m?i c?a Camera
            Vector3 targetPosition = player.position + offset;
            targetPosition.y = transform.position.y; // Gi? nguy�n chi?u cao Y

            // D?ch chuy?n m??t m�
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
