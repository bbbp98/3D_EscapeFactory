using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Transform[] obstaclePoints;
    public Transform[] starPoints;

    private void OnDrawGizmos()
    {
        if (obstaclePoints == null)
            return;

        foreach (Transform t in obstaclePoints)
        {
            if (t == null) continue;

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(t.position, 0.3f);
        }

        foreach (Transform t in starPoints)
        {
            if (t == null) continue;

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(t.position, 0.3f);
        }
    }
}
