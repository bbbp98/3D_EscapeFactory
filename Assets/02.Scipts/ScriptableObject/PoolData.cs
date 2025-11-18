using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PoolData
{
    public string key;
    public GameObject prefab;
    /// <summary>
    /// 초기 pool의 크기
    /// </summary>
    public int poolSize;

    /// <summary>
    /// 실제 pool 역할(오브젝트 보관)
    /// </summary>
    [HideInInspector] public Queue<GameObject> pool = new Queue<GameObject>();
}
