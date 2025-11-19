using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour, IPoolObject
{
    [Header("Item")]
    [SerializeField] protected ItemData data;
    [SerializeField] private float rotateSpeed = 100f;

    public string Key { get; set; }

    [Header("Magnet")]
    private bool isMagnet = false;
    [SerializeField] private float maxDistance;
    [SerializeField] private float magnetSpeed = 15f;

    private void Update()
    {
        TryMagnet();
        Rotate();
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

    #region Pool Method
    public void OnSpawnFromPool()
    {
        transform.rotation = Quaternion.identity;
        isMagnet = false;
    }

    public void OnReturnToPool()
    {
        transform.rotation = Quaternion.identity;
        isMagnet = false;
    }
    #endregion

    private void Rotate()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }

    private void TryMagnet()
    {
        // player의 마그넷 체크

        Transform target = null;   // player위치 필요
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);
        if (distance > maxDistance) return;

        transform.position = Vector3.MoveTowards(transform.position, target.position, magnetSpeed * Time.deltaTime);
    }
}
