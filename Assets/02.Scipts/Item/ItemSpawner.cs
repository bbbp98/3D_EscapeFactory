using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("StarPrefab")]
    [SerializeField] private GameObject starPrefab;

    /// <summary>
    /// 장애물이 없는 레인에 점수를 획득할 수 있는 별 아이템 생성
    /// </summary>
    /// <param name="tile">아이템을 생성할 타일 정보</param>
    /// <param name="laneBlocked">레인에 장애물이 있는지에 대한 정보가 담긴 배열</param>
    public void SpawnStarsInTile(Tile tile, bool[] laneBlocked)
    {
        if (tile == null) return;

        for (int lane = 0; lane < tile.lanes.Length; lane++)
        {
            if (laneBlocked[lane]) continue;

            foreach (var point in tile.lanes[lane].starPoints)
            {
                GameObject go = PoolManager.Instance.Get(starPrefab.name);
                go.transform.SetPositionAndRotation(point.position, point.rotation);

                Transform parent = Application.isPlaying ? tile.dynamicRoot : tile.transform;
                go.transform.SetParent(parent);
            }
        }
    }
}
