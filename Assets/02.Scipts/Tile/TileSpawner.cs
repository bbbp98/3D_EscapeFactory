using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [Header("Setting Tiles")]
    [SerializeField] private Tile[] tilePrefabs;
    [SerializeField] private int tilesOnSceen;
    private float tileLength = 23f;

    [SerializeField] private ObstacleSpawner obstacleSpawner;
    [SerializeField] private ItemSpawner itemSpawner;

    [Header("Player Transform")]
    [SerializeField] Transform playerTr;

    private float spawnZ = 46f;

    private Queue<Tile> activeTiles = new Queue<Tile>();
    private bool[] trackBlocked = new bool[5];


    private void Start()
    {
        for (int i = 0; i < tilesOnSceen; i++)
        {
            SpawnTile();
        }
    }

    private void Update()
    {
        if (playerTr.position.z - (tileLength * 2) > spawnZ - (tilesOnSceen * tileLength))
        {
            SpawnTile();
            DisableTile();
        }
    }

    private void SpawnTile()
    {
        Tile prefab = tilePrefabs[Random.Range(0, tilePrefabs.Length)];
        Tile tile = Instantiate(prefab, Vector3.forward * spawnZ, Quaternion.identity);

        spawnZ += tileLength;

        activeTiles.Enqueue(tile);
        trackBlocked = obstacleSpawner.SpawnObstaclesInTile(tile);
        itemSpawner.SpawnStarsInTile(tile, trackBlocked);
    }

    private void DisableTile()
    {
        Destroy(activeTiles.Dequeue().gameObject);
    }
}
