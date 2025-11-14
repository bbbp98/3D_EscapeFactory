using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float spawnRate = 0.5f;

    [Header("Obstacle Prefabs")]
    [SerializeField] private GameObject[] obstaclePrefabs;

    public void SpawnObstaclesInTile(Tile tile)
    {
        if (tile == null) return;

        foreach (Transform point in tile.obstaclePoints)
        {
            float rand = Random.value;

            // spawn obstacle
            if (rand < spawnRate)
            {
                GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
                Instantiate(prefab, point.position, point.rotation, tile.transform);
            }
        }
    }
}
