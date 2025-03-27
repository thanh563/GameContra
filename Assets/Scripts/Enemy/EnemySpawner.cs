using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign in Inspector
    public int totalEnemies = 10;  // Initial number of enemies
    public float spawnInterval = 1.5f; // Time between spawns
    public float waveDelay = 10f; // Time to wait before the next wave
    public int wave = 0;
    public bool randomizeX = true; // Toggle for random X position

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        if (enemyPrefab == null)
        {
            //Debug.LogError("Enemy Prefab is not assigned in the Inspector!");
            return;
        }

        //Debug.Log("Starting Enemy Waves...");
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true) // Infinite spawning loop
        {
            //Debug.Log($"Spawning Wave {wave + 1} with {totalEnemies} enemies...");
            yield return StartCoroutine(SpawnEnemies());

            // Wait before the next wave
            yield return new WaitForSeconds(waveDelay);

            // Increase enemy count and wave number
            totalEnemies += 10;
            wave++;
        }
    }

    IEnumerator SpawnEnemies()
    {
        //Debug.Log("Spawning Enemies...");
        for (int i = 0; i < totalEnemies; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy Prefab is missing!");
            return;
        }

        // Calculate a valid spawn position
        Vector3 spawnPosition = GetSpawnPosition();

        // Instantiate enemy
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        //Debug.Log($"Enemy spawned at {spawnPosition}");

        // Apply health scaling if the enemy has an EnemyManager component
        EnemyManager enemyManager = enemy.GetComponent<EnemyManager>();
        if (enemyManager != null)
        {
            enemyManager.health = Mathf.Max(1, (int)(enemyManager.health * (0.5f * (wave + 1))));
            //Debug.Log($"Enemy health set to {enemyManager.health}");
        }
    }

    Vector3 GetSpawnPosition()
    {
        float cameraRightEdge = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect;
        float spawnX = randomizeX
            ? Random.Range(cameraRightEdge + 5f, cameraRightEdge + 10f) // Random X outside camera
            : cameraRightEdge + 10f; // Fixed X

        float spawnY = 1f;
        return new Vector3(spawnX, spawnY, 0f);
    }
}
