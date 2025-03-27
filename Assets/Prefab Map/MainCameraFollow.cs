using UnityEngine;

public class MainCameraFollow : MonoBehaviour
{
    public Transform player;  // Player c?n theo dõi
    public float smoothSpeed = 5f; // T?c ?? m??t mà

    private Vector3 offset; // ?? l?ch gi?a Player và Camera

    void Start()
    {
        if (player != null)
        {
            // L?y kho?ng cách ban ??u gi?a Camera và Player
            offset = transform.position - player.position;
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Tính toán v? trí m?i c?a Camera
            Vector3 targetPosition = player.position + offset;
            targetPosition.y = transform.position.y; // Gi? nguyên chi?u cao Y

            // D?ch chuy?n m??t mà
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
