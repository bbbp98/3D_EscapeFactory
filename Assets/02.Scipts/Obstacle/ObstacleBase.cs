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

    private void OnTriggerEnter(Collider other)
    {
        // 충돌 체크

    }

    protected virtual void OnHitEffect() { }    // 충돌했을 때 이펙트
}
