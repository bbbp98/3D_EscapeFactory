using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    [SerializeField] private ItemData data;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerCondition>(out PlayerCondition player))
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 플레이어가 아이템을 획득했을 때의 효과를 실행합니다.
    /// </summary>
    protected virtual void OnGetEffect() { }
}
