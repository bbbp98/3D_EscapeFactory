using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSlow : ObstacleBase
{
    [SerializeField] private float slowValue = 0.5f;
    [SerializeField] private float duration = 3f;

    private void Reset()
    {
        type = ObstacleType.Slow;
    }

    /// <summary>
    /// 플레이어를 느려지게 합니다.
    /// </summary>
    /// <param name="player">플레이어의 정보</param>
    protected override void OnHitEffect(PlayerCondition player)
    {
        base.OnHitEffect(player);
        player.Slow(slowValue, duration);
    }
}
