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
        if (other.CompareTag("Player"))
        {
            //isConflict = true;
            Debug.Log("coll");
        }
    }

    protected virtual void OnHitEffect() { }    // 충돌했을 때 이펙트
}
