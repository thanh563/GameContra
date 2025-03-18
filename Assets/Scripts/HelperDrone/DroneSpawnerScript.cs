using System.Collections.Generic;
using UnityEngine;

public class DroneSpawnerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject dronePrefab;
    [SerializeField]
    private float spawnInterval = 5f;
    [SerializeField]
    private int spawnSize = 2;
    [SerializeField]
    private GameObject player;

    private List<GameObject> drones;
    void Start()
    {
        drones = new List<GameObject>();
        for (int i = 0; i < spawnSize; i++)
        {
            GameObject drone = Instantiate(dronePrefab);
            drones.Add(drone);
        }

        InvokeRepeating(nameof(SpawnEnemy), 0, spawnInterval);
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < spawnSize; i++)
        {
            HelperDroneScript droneScript = drones[i].GetComponent<HelperDroneScript>();

            if (droneScript.available)
            {
                bool spawnLeft = Random.value > 0.5f;
                drones[i].transform.position = GetRandomSpawnPosition(spawnLeft);
                droneScript.movingRight = !spawnLeft; // Opposite direction
                drones[i].SetActive(true);
                break;
            }
        }
    }

    Vector3 GetRandomSpawnPosition(bool spawnLeft)
    {
        Camera cam = Camera.main;
        float height = cam.orthographicSize;
        float width = height * cam.aspect;

        // Spawn position relative to the player's x-position
        float xPos = spawnLeft ? player.transform.position.x - width - 1f : player.transform.position.x + width + 1f;

        // Keep it in the upper half of the screen relative to the player's y-position
        float yPos = player.transform.position.y + Random.Range(1, height - 4f);

        return new Vector3(xPos, yPos, 0f);
    }


}
