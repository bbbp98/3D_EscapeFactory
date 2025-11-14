using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleInstantDeath : ObstacleBase
{
    private void Reset()
    {
        type = ObstacleType.InstantDeath;
    }

    protected override void OnHitEffect()
    {
    }
}
