using UnityEngine;

public class ItemShieldAndSpeedUp : ItemBase
{
    [SerializeField] private float speedValue;
    [SerializeField] private float duration;

    protected override void OnGetEffect(PlayerCondition player)
    {
        // player에 무적 및 속도 증가 메서드 호출
    }
}
