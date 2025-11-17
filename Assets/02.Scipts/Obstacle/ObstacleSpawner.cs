using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float spawnRate = 0.5f;

    [Header("Obstacle Prefabs")]
    [SerializeField] private GameObject[] obstaclePrefabs;

    private bool[] trackBlocked = new bool[5];

    /// <summary>
    /// 랜덤 장애물을 생성합니다.
    /// </summary>
    /// <param name="tile"></param>
    /// <returns>해당 레인에 장애물이 있는 지 판단하는 bool타입의 배열</returns>
    public bool[] SpawnObstaclesInTile(Tile tile)
    {
        if (tile == null) return null;

        ResetTrackBlock();

        for (int i = 0; i < tile.lanes.Length; i++)
        {
            foreach (var point in tile.lanes[i].obstaclePoints)
            {
                if (point == null) continue;

                float rand = Random.value;

                if (rand < spawnRate)
                {
                    GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
                    Instantiate(prefab, point.position, point.rotation, tile.transform);
                    trackBlocked[i] = true;
                }
            }
        }

        return trackBlocked;
    }

    private void ResetTrackBlock()
    {
        for (int i = 0; i < 5; i++)
            trackBlocked[i] = false;
    }
}
