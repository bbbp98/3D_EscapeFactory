using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour, IPoolObject
{
    [SerializeField] protected ItemData data;
    [SerializeField] private float rotateSpeed = 100f;

    public string Key { get; set; }

    private void Update()
    {
        Rotate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerCondition>(out PlayerCondition player))
        {
            //Destroy(gameObject);
            PoolManager.Instance.Release(gameObject);
            OnGetEffect(player);
        }
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 플레이어가 아이템을 획득했을 때의 효과
    /// </summary>
    protected virtual void OnGetEffect(PlayerCondition player) { }

    public void OnSpawnFromPool()
    {
        transform.rotation = Quaternion.identity;
    }

    public void OnReturnToPool()
    {
        transform.rotation = Quaternion.identity;
    }
}
