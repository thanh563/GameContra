using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public GameObject player;        // Tham chi?u ??n player
    public float smoothSpeed = 0.125f; // T?c ?? m??t mà khi camera di chuy?n (0-1, nh? h?n = m??t h?n)
    public Vector2 offset;           // ?? l?ch c?a camera so v?i player (n?u c?n)

    void LateUpdate()
    {
        // V? trí m?c tiêu c?a camera (ch? theo tr?c X, gi? nguyên Y)
        Vector3 desiredPosition = new Vector3(
            player.transform.position.x + offset.x,  // Theo X c?a player
            transform.position.y,                    // Gi? nguyên Y c?a camera
            transform.position.z                     // Gi? nguyên Z (th??ng là -10 cho camera 2D)
        );

        // T?o chuy?n ??ng m??t mà b?ng Lerp
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // C?p nh?t v? trí camera
        transform.position = smoothedPosition;
    }
}