using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _SceneManager : MonoBehaviour
{
    public static _SceneManager Instance { get; private set; }

    [Header("Scenes")]

    public SerializableDictionary<SceneType, SceneAsset> scenes;

    private Dictionary<SceneType, SceneAsset> sceneDic = new Dictionary<SceneType, SceneAsset>();

    void Awake()
    {
        // 싱글톤
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        sceneDic = scenes.ToDictionary();
    }

    void Start()
    {
        OpenScene(SceneType.Title);
        UIManager.Instance.QuickUIOnOff(PanelType.InGame, false);
        //StartCoroutine(GameManager.Instance.CountDown());
        
    }

    public void LoadGame()
    {
        UIManager.Instance.QuickUIOnOff(PanelType.InGame, true);
        UIManager.Instance.QuickUIOnOff(PanelType.GameOver, false);
        UIManager.Instance.QuickUIOnOff(PanelType.Settings, false);
        SceneManager.LoadScene(sceneDic[SceneType.Game].name);
        StartCoroutine(GameManager.Instance.CountDown());
    }


    public void OpenScene(SceneType st)
    {
        SceneManager.LoadScene(sceneDic[st].name);
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene(sceneDic[SceneType.Title].name);
        UIManager.Instance.QuickUIOnOff(PanelType.InGame, false);
        UIManager.Instance.QuickUIOnOff(PanelType.GameOver, false);
        UIManager.Instance.QuickUIOnOff(PanelType.Settings, false);
    }
}
