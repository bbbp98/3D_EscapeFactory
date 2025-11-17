using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("ItemPrefabs")]
    [SerializeField] private GameObject starPrefab;

    public void SpawnStarsInTile(Tile tile, bool[] trackBlocked)
    {
        if (tile == null) return;

        int i = 0;
        int index = 0;
        foreach (Transform point in tile.starPoints)
        {
            if (!trackBlocked[index] && point != null)
                Instantiate(starPrefab, point.position, point.rotation, tile.transform);
            i++;
            if (i >= 10)
            {
                i -= 10;
                index++;
            }
        }
    }
}
