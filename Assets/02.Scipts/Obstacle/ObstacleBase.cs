using UnityEngine;

public enum ObstacleType
{
    None,
    Slow,
    InstantDeath,
}

public class ObstacleBase : MonoBehaviour, IPoolObject
{
    [SerializeField] protected ObstacleType type;

    private bool isConflict = false;
    public string Key { get; set; }
    

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
            OnHitEffect(player);

            PoolManager.Instance.Release(gameObject);
            return;
        }
    }

    /// <summary>
    /// 플레이어와 충돌이 일어났을 때의 효과를 실행.
    /// 체력 감소는 Base에서 실행
    /// ex) 이동속도 저하
    /// </summary>
    /// <param name="player">플레이어의 정보</param>
    protected virtual void OnHitEffect(PlayerCondition player)  // 충돌했을 때 이펙트
    {
        player.Damaged();
    }

    public void OnSpawnFromPool()
    {
        isConflict = false;
    }

    public void OnReturnToPool()
    {
        isConflict = false;
    }
}
