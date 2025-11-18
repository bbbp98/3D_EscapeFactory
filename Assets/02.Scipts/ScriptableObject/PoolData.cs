using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PoolData
{
    public string key;
    public GameObject prefab;
    public int poolSize;

    [HideInInspector] public Queue<GameObject> pool = new Queue<GameObject>();
}
