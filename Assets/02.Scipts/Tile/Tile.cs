using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TileLane
{
    /// <summary>
    /// 장애물이 설치될 위치
    /// </summary>
    public Transform[] obstaclePoints;
    /// <summary>
    /// 별(점수 획득 아이템)이 설치될 위치
    /// </summary>
    public Transform[] starPoints;
}

public class Tile : MonoBehaviour
{
    /// <summary>
    /// 장애물이나 아이템이 설치될 레인
    /// </summary>
    public TileLane[] lanes = new TileLane[5];

    private void OnDrawGizmos()
    {
        foreach (var lane in lanes)
        {
            if (lane == null) continue;

            foreach (Transform t in lane.obstaclePoints)
            {
                if (t == null) continue;

                Gizmos.color = Color.red;
                Gizmos.DrawSphere(t.position, 0.3f);
            }

            foreach (Transform t in lane.starPoints)
            {
                if (t == null) continue;

                Gizmos.color = Color.green;
                Gizmos.DrawSphere(t.position, 0.3f);
            }
        }
    }
}
