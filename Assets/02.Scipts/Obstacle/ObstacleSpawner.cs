using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Obstacle Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float baseSpawnRate = 0.3f;

    [Header("Obstacle Prefabs")]
    [SerializeField] private GameObject[] obstaclePrefabs;

    private bool[] laneBlocked = new bool[5];

    /// <summary>
    /// 랜덤 장애물을 생성
    /// </summary>
    /// <param name="tile"></param>
    /// <returns>해당 레인에 장애물이 있는 지 판단하는 bool타입의 배열</returns>
    public bool[] SpawnObstaclesInTile(Tile tile)
    {
        if (tile == null) return null;

        ResetLane();

        for (int lane = 0; lane < tile.lanes.Length; lane++)
        {
            foreach (var point in tile.lanes[lane].obstaclePoints)
            {
                if (point == null) continue;

                float newSpawnRate = baseSpawnRate * GameManager.Instance.TotalSpeedMultiplier();
                newSpawnRate = Mathf.Min(newSpawnRate, 0.9f);

                if (Random.value < newSpawnRate)
                {
                    GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
                    GameObject go = PoolManager.Instance.Get(prefab.name);
                    go.transform.SetPositionAndRotation(point.position, point.rotation);

                    Transform parent = Application.isPlaying ? tile.dynamicRoot : tile.transform;
                    go.transform.SetParent(parent);

                    laneBlocked[lane] = true;
                }
            }
        }

        return laneBlocked;
    }

    /// <summary>
    /// 레인에 장애물이 있는지 bool이 담긴 laneBlocked 초기화
    /// </summary>
    private void ResetLane()
    {
        for (int i = 0; i < 5; i++)
            laneBlocked[i] = false;
    }
}
