using UnityEngine;

public class ItemBase : MonoBehaviour, IPoolObject
{
    [Header("Item")]
    [SerializeField] protected ItemData data;
    [SerializeField] private float rotateSpeed = 100f;

    public string Key { get; set; }

    [Header("Magnet")]
    private float maxDistance = 10f;
    float magnetSpeed = 40f;
    private bool isAttracted = false;
    private float curSpeed = 0f;

    public static Transform magnetTarget;
    public static bool magnetEnabled = false;

    private void Update()
    {
        Rotate();
        MagnetMove();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerCondition>(out PlayerCondition player))
        {
            PoolManager.Instance.Release(gameObject);
            OnGetEffect(player);
        }
    }

    /// <summary>
    /// 플레이어가 아이템을 획득했을 때의 효과
    /// </summary>
    protected virtual void OnGetEffect(PlayerCondition player) { }

    #region Pool Method Initialize
    public void OnSpawnFromPool()
    {
        transform.rotation = Quaternion.identity;
        isAttracted = false;
    }

    public void OnReturnToPool()
    {
        transform.rotation = Quaternion.identity;
        isAttracted = false;
    }
    #endregion

    private void Rotate()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }

    #region Magnet
    public static void SetMagnetTarget(Transform target)
    {
        magnetTarget = target;
        magnetEnabled = true;
    }

    public static void ClearMagnetTarget()
    {
        //magnetTarget = null;
        magnetEnabled = false;
    }

    private void MagnetMove()
    {
        if (magnetTarget == null) return;

        Vector3 targetPosition = magnetTarget.position + Vector3.up;
        float distance = Vector3.Distance(transform.position, targetPosition);

        if (!isAttracted && magnetEnabled && distance <= maxDistance)
            isAttracted = true;

        #region Item Move
        if (!isAttracted) return;

        float accel = 25f;
        curSpeed = Mathf.Lerp(curSpeed, magnetSpeed, Time.deltaTime * accel);

        float distanceFactor = Mathf.Clamp01(1f - (distance / maxDistance));

        float boostedSpeed = curSpeed * (1f + distanceFactor);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, boostedSpeed * Time.deltaTime);

        if (transform.position.z < targetPosition.z - 1f)
            transform.position = targetPosition;
        #endregion
    }
    #endregion
}
