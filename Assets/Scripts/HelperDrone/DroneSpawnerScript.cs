using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine;

public class DroneSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject dronePrefab;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private int spawnSize = 2;
    [SerializeField] private GameObject target;
    private List<GameObject> drones;

    void Start()
    {
        drones = new List<GameObject>();
        for (int i = 0; i < spawnSize; i++)
        {
            GameObject drone = Instantiate(dronePrefab);
            drones.Add(drone);
            drone.SetActive(false); // Ensure they are disabled initially
        }

        InvokeRepeating(nameof(SpawnEnemy), 0, spawnInterval);
    }

    void SpawnEnemy()
    {
        foreach (var drone in drones)
        {
            LootDroneScript droneScript = drone.GetComponent<LootDroneScript>();

            if (droneScript.available)
            {
                //bool spawnLeft = Random.value > 0.5f;
                Vector3 spawnPos = GetSpawnPosition();

                drone.transform.position = spawnPos;
                droneScript.movingRight = false; // Move opposite to spawn side
                //drone.GetComponent<EnemyDroneGunScript>().target = target;
                drone.SetActive(true);
                return;
            }
        }
    }

    Vector3 GetRandomSpawnPosition(bool spawnLeft)
    {
        Camera cam = Camera.main;
        if (cam == null) return Vector3.zero; // Safety check

        float height = cam.orthographicSize;
        float width = height * cam.aspect;

        // Get the actual camera position (since Cinemachine might move it dynamically)
        Vector3 camPos = cam.transform.position;

        float xPos = spawnLeft ? camPos.x - width - 1f : camPos.x + width + 1f;
        float yPos = camPos.y + Random.Range(0f, height - 1.5f);

        return new Vector3(xPos, yPos, 0f);
    }

    Vector3 GetSpawnPosition()
    {
        Camera cam = Camera.main;
        float cameraRightEdge = cam.transform.position.x + cam.orthographicSize * cam.aspect;
        float spawnX = Random.Range(cameraRightEdge + 5f, cameraRightEdge + 10f); // Random X outside camera
        
        Vector3 camPos = cam.transform.position;
        float yPos = camPos.y + Random.Range(0f, cam.orthographicSize - 1.5f);

        return new Vector3(spawnX, yPos, 0f);
    }
}
