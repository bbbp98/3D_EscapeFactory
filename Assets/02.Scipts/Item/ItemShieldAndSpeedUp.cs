using UnityEngine;

public class ItemShieldAndSpeedUp : ItemBase
{
    protected override void OnGetEffect(PlayerCondition player)
    {
        // player에 무적 및 속도 증가 메서드 호출
        player.ShieldAndBoost(data.value, data.buffDuration);
    }
}
