using UnityEngine;

public enum ObstacleType
{
    None,
    Slow,
    InstantDeath,
}

public class ObstacleBase : MonoBehaviour
{
    [SerializeField] protected ObstacleType type;
    private bool isConflict = false;

    private void Reset()
    {
        type = ObstacleType.None;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isConflict)
            return;

        // 충돌 체크 (component 검사로 변경 예정)
        if (other.TryGetComponent<PlayerCondition>(out PlayerCondition player))
        {
            isConflict = true;  // 중복 충돌 방지
            Debug.Log("coll");
            Destroy(gameObject);
            // player랑 컴포넌트 검사로 충돌 체크
            // player 피격 애니메이션
            // 체력 감소되는 메서드
            OnHitEffect(player);
        }
    }

    /// <summary>
    /// 플레이어와 충돌이 일어났을 때의 효과를 실행합니다.
    /// ex) 체력 감소, 이동속도 저하
    /// </summary>
    /// <param name="player">플레이어의 정보</param>
    protected virtual void OnHitEffect(PlayerCondition player) { }    // 충돌했을 때 이펙트
}
