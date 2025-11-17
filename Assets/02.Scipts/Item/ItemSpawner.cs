using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("StarPrefab")]
    [SerializeField] private GameObject starPrefab;

    /// <summary>
    /// 장애물이 없는 레인에 점수를 획득할 수 있는 별 아이템을 생성합니다.
    /// </summary>
    /// <param name="tile">아이템을 생성할 타일 정보</param>
    /// <param name="trackBlocked">레인에 장애물이 있는지에 대한 정보가 담긴 배열</param>
    public void SpawnStarsInTile(Tile tile, bool[] trackBlocked)
    {
        if (tile == null) return;

        for (int i = 0; i < tile.lanes.Length; i++)
        {
            foreach (var point in tile.lanes[i].starPoints)
            {
                if (trackBlocked[i]) break;

                if (point == null) continue;

                Instantiate(starPrefab, point.position, point.rotation, tile.transform);
            }
        }
    }
}
