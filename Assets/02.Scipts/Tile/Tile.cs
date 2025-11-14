using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Transform[] obstaclePoints;

    #region test code
    public ObstacleSpawner os;

    private void Start()
    {
        os.SpawnObstaclesInTile(this);
    }
    #endregion
}
