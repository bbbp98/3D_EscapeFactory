using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float spawnRate = 0.5f;

    [Header("Obstacle Prefabs")]
    [SerializeField] private GameObject[] obstaclePrefabs;

    private bool[] trackBlocked = new bool[5];

    public bool[] SpawnObstaclesInTile(Tile tile)
    {
        for (int i = 0; i < 5; i++)
            trackBlocked[i] = false;

        if (tile == null) return null;

        foreach (Transform point in tile.obstaclePoints)
        {
            if (point == null) continue;

            float rand = Random.value;

            // spawn obstacle
            if (rand < spawnRate)
            {
                GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
                Instantiate(prefab, point.position, point.rotation, tile.transform);
                int index = ConvertPointToTrack(point.position.x);
                trackBlocked[index] = true;
            }
        }

        return trackBlocked;
    }

    private int ConvertPointToTrack(float posX)
    {
        switch (posX)
        {
            case -5.46f:
                return 0;
            case -2.73f:
                return 1;
            case 0f:
                return 2;
            case 2.73f:
                return 3;
            case 5.46f:
                return 4;
            default:
                return -1;
        }
    }
}
