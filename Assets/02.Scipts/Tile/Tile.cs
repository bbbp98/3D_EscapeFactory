using System;
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

public class Tile : MonoBehaviour, IPoolObject
{
    /// <summary>
    /// 장애물이나 아이템이 설치될 레인
    /// </summary>
    public TileLane[] lanes = new TileLane[5];

    /// <summary>
    /// 동적 생성되는 오브젝트의 parent위치
    /// </summary>
    [SerializeField] public Transform dynamicRoot;

    public string Key { get; set; }

    public void OnReturnToPool()
    {
        for (int i = dynamicRoot.childCount - 1; i >= 0; i--)
        {
            Transform child = dynamicRoot.GetChild(i);
            PoolManager.Instance.Release(child.gameObject);
        }
    }

    public void OnSpawnFromPool()
    {
        gameObject.SetActive(true);
    }

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
