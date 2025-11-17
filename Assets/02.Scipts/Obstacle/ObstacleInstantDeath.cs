using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleInstantDeath : ObstacleBase
{
    private void Reset()
    {
        type = ObstacleType.InstantDeath;
    }

    /// <summary>
    /// 플레이어를 즉사시킵니다.
    /// </summary>
    /// <param name="player">플레이어의 정보</param>
    protected override void OnHitEffect(PlayerCondition player)
    {
        player.InstantDeath();
    }
}
