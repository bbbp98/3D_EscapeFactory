using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSlow : ObstacleBase
{
    [SerializeField] private float slowValue = 0.5f;
    [SerializeField] private float duration = 1.5f;

    private void Reset()
    {
        type = ObstacleType.Slow;
    }

    protected override void OnHitEffect()
    {
    }
}
