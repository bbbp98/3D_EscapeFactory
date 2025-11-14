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
        os = FindFirstObjectByType<ObstacleSpawner>();
        os.SpawnObstaclesInTile(this);
    }
    #endregion

    private void OnDrawGizmos()
    {
        if (obstaclePoints == null)
            return;

        foreach (Transform t in obstaclePoints)
        {
            if (t == null) continue;

            Gizmos.DrawSphere(t.position, 0.3f);
        }
    }
}
