using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager instance;
    public static PoolManager Instance { get { return instance; } }

    [SerializeField] private List<PoolData> pools = new List<PoolData>();
    private Dictionary<string, PoolData> poolDict = new Dictionary<string, PoolData>();

    private Transform poolRoot;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        CreatePoolRoot();
        Initialize();
    }

    /// <summary>
    /// pool에 생성된 GameObject 정리용 Root 생성
    /// </summary>
    private void CreatePoolRoot()
    {
        GameObject root = GameObject.Find("[PoolRoot]");

        if (root == null)
        {
            root = new GameObject("[PoolRoot]");
            root.transform.SetParent(null);
        }

        poolRoot = root.transform;
        //DontDestroyOnLoad(poolRoot.gameObject);
    }

    private void Initialize()
    {
        foreach (PoolData p in pools)
        {
            poolDict[p.key] = p;

            for (int i = 0; i < p.poolSize; i++)
            {
                GameObject go = Instantiate(p.prefab);
                go.SetActive(false);
                go.transform.SetParent(poolRoot);

                if (go.TryGetComponent<IPoolObject>(out var poolObj))
                    poolObj.Key = p.key;

                p.pool.Enqueue(go);
            }
        }
    }

    /// <summary>
    /// pool에서 GameObject 가져오기
    /// </summary>
    /// <param name="key">Prefab.name</param>
    public GameObject Get(string key)
    {
        if (!poolDict.ContainsKey(key))
        {
            Debug.Log($"pool not found: {key}");
            return null;
        }

        PoolData p = poolDict[key];
        GameObject go;

        if (p.pool.Count > 0)
        {
            go = p.pool.Dequeue();
        }
        else
        {
            go = Instantiate(p.prefab);

            if (go.TryGetComponent<IPoolObject>(out var poolObj))
                poolObj.Key = key;
        }

        go.SetActive(true);
        
        if (go.TryGetComponent<IPoolObject>(out var poolObject))
            poolObject.OnSpawnFromPool();

        return go;
    }

    /// <summary>
    /// pool에 GameObject 반납
    /// </summary>
    /// <param name="go">반납할 GameObject</param>
    public void Release(GameObject go)
    {
        if (!go.TryGetComponent<IPoolObject>(out var poolObj))
        {
            Destroy(go);
            return;
        }

        string key = poolObj.Key;

        if (!poolDict.ContainsKey(key))
        {
            Destroy(go);
            return;
        }

        poolObj.OnReturnToPool();
        go.SetActive(false);
        go.transform.SetParent(poolRoot);

        poolDict[key].pool.Enqueue(go);
    }
}
