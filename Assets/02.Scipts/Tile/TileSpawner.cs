using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [Header("Setting Tiles")]
    [SerializeField] private Tile[] tilePrefabs;
    [SerializeField] private int tilesOnSceen;

    private float tileLength = 23f;
    private float spawnZ = 46f;

    [Header("Spawner")]
    [SerializeField] private ObstacleSpawner obstacleSpawner;
    [SerializeField] private ItemSpawner itemSpawner;

    [Header("Player Transform")]
    [SerializeField] private Transform playerTr;

    private Queue<Tile> activeTiles = new Queue<Tile>();
    private bool[] laneBlocked = new bool[5];

    private Transform tileRoot;

    private void Start()
    {
        Restart();
    }

    private void Update()
    {
        if (playerTr.position.z - (tileLength * 2) > spawnZ - (tilesOnSceen * tileLength))
        {
            SpawnTile();
            DisableTile();
        }
    }

    /// <summary>
    /// 랜덤한 타일을 생성합니다.
    /// </summary>
    private void SpawnTile()
    {
        //Tile prefab = tilePrefabs[Random.Range(0, tilePrefabs.Length)];
        //Tile tile = Instantiate(prefab, Vector3.forward * spawnZ, Quaternion.identity);

        //spawnZ += tileLength;

        //activeTiles.Enqueue(tile);
        //trackBlocked = obstacleSpawner.SpawnObstaclesInTile(tile);
        //itemSpawner.SpawnStarsInTile(tile, trackBlocked);

        Tile prefab = tilePrefabs[Random.Range(0, tilePrefabs.Length)];

        Tile tile = PoolManager.Instance.Get(prefab.name).GetComponent<Tile>();
        tile.transform.position = Vector3.forward * spawnZ;

        spawnZ += tileLength;

        activeTiles.Enqueue(tile);

        laneBlocked = obstacleSpawner.SpawnObstaclesInTile(tile);
        itemSpawner.SpawnStarsInTile(tile, laneBlocked);
    }

    private void DisableTile()
    {
        //Destroy(activeTiles.Dequeue().gameObject);

        Tile oldTile = activeTiles.Dequeue();
        PoolManager.Instance.Release(oldTile.gameObject);
    }

    public void Restart()
    {
        spawnZ = 46f;

        for (int i = 0; i < tilesOnSceen; i++)
        {
            SpawnTile();
        }
    }
}
