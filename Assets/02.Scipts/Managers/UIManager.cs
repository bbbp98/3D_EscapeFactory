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

    [Header("UI parameters")]

    public float popupTime;
    public float popupRatio;

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

    // 버튼 OnClick 전용
    public void OpenUI(GameObject go)
    {
        // 패널 활성화
        StartCoroutine(PopUI(go, true));
        //panelDic[pt].SetActive(true);
    }

    // 버튼 OnClick 전용
    public void CloseUI(GameObject go)
    {
        // 패널 비활성화
        StartCoroutine(PopUI(go, false));
        //panelDic[pt].SetActive(false);
    }

    // 타 클래스에서 호출하는 용
    public void CallUIOnOff(PanelType pt, bool tf)
    {
        StartCoroutine(PopUI(panelDic[pt], tf));
    }

    public void SetBgmVolume(float v)
    {
        SoundManager.Instance.ChangeBgmVolume(v);
    }

    public void SetSfxVolume(float v)
    {
        SoundManager.Instance.ChangeSfxVolume(v);
    }

    IEnumerator PopUI(GameObject panel, bool tf)
    {
        float t = 0f;
        float ratio = 0f;

        if (tf)
        {
            if (!panel.activeInHierarchy)
            {
                panel.SetActive(true);
            }
        }

        if(panel.TryGetComponent<RectTransform>(out RectTransform rtrans))
        {
            Debug.Log("get rect transform");
            while(t < popupTime)
            {
                t += Time.fixedDeltaTime;

                ratio = tf ? Mathf.Lerp(popupRatio, 1f, t / popupTime) : Mathf.Lerp(1f, popupRatio, t / popupTime);
            
                rtrans.localScale = new Vector3( ratio, ratio, 1);
                yield return new WaitForFixedUpdate();
            }        
        }

        if (!tf)
        {
            panel.SetActive(false);
        }
    }

}
