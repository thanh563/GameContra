using UnityEngine;

public class InfiniteMap : MonoBehaviour
{
    public Transform player;  // Player
    public Transform ground1; // Ground 1 (25f)
    public Transform ground2; // Ground 2 (25f)
    [SerializeField]
    private float groundWidth = 25f; // Chi?u r?ng c?a m?i n?n ??t
    private Transform lastGround; // N?n ??t cu?i cùng mà Player ?ã ?i qua

    void Start()
    {
        lastGround = ground2; // M?c ??nh Ground2 là n?n cu?i cùng
    }

    void Update()
    {
        // Ki?m tra n?u Player s?p ??n 30% mép c?a n?n ??t cu?i cùng
        if (player.position.x >= lastGround.position.x - (groundWidth * 0.3f))
        {
            // Xác ??nh n?n ??t còn l?i
            Transform nextGround = (lastGround == ground1) ? ground2 : ground1;

            // Di chuy?n n?n còn l?i ra tr??c
            nextGround.position = new Vector3(lastGround.position.x + groundWidth, nextGround.position.y, nextGround.position.z);

            // C?p nh?t n?n ??t cu?i cùng
            lastGround = nextGround;
        }
    }
}
