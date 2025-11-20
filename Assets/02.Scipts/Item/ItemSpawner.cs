using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Star")]
    [SerializeField] private GameObject starPrefab;

    [Header("Item")]
    [Range(0f, 1f)]
    [SerializeField] private float spawnRate = 0.1f;
    [SerializeField] private GameObject[] itemPrefabs;

    /// <summary>
    /// 장애물이 없는 레인에 점수를 획득할 수 있는 별 아이템 생성
    /// </summary>
    /// <param name="tile">아이템을 생성할 타일 정보</param>
    /// <param name="laneBlocked">레인에 장애물이 있는지에 대한 정보가 담긴 배열</param>
    public void SpawnStarsInTile(Tile tile, int lane)
    {
        if (tile == null || lane < 0) return;

        foreach (var point in tile.lanes[lane].starPoints)
        {
            GameObject go = PoolManager.Instance.Get(starPrefab.name);
            go.transform.SetPositionAndRotation(point.position, point.rotation);

            Transform parent = Application.isPlaying ? tile.dynamicRoot : tile.transform;
            go.transform.SetParent(parent);
        }
    }

    public void SpawnItemsInTile(Tile tile, int lane)
    {
        if (tile == null || lane < 0) return;

        foreach (var point in tile.lanes[lane].itemPoints)
        {
            if (Random.value < spawnRate)
            {
                GameObject go = PoolManager.Instance.Get(itemPrefabs[Random.Range(0, itemPrefabs.Length)].name);
                go.transform.SetPositionAndRotation(point.position, point.rotation);

                Transform parent = Application.isPlaying ? tile.dynamicRoot : tile.transform;
                go.transform.SetParent(parent);
            }
        }
    }
}
