using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue>
{
    [Serializable]
    public struct Pair
    {
        public TKey key;
        public TValue value;
    }

    public List<Pair> pairs = new List<Pair>();

    public Dictionary<TKey, TValue> ToDictionary()
    {
        Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>();
        foreach (var p in pairs)
        {
            if (!result.ContainsKey(p.key))
                result.Add(p.key, p.value);
        }
        return result;
    }
}
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Panels")]
    // 인스펙터 전용 커스텀 사전
    public SerializableDictionary<PanelType, GameObject> panels;
    // 실제 사용 사전
    private Dictionary<PanelType, GameObject> panelDic = new Dictionary<PanelType, GameObject>();

    void Awake()
    {
        // 싱글톤
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // 사전 초기화 
        panelDic = panels.ToDictionary();
    }

    public void OpenUI(PanelType pt)
    {
        // 패널 활성화
        panelDic[pt].SetActive(true);
    }

    public void CloseUI(PanelType pt)
    {
        // 패널 비활성화
        panelDic[pt].SetActive(false);
    }

}
