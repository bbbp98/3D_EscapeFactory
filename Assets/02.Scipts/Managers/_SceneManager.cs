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
    }

    void Start()
    {
        sceneDic = scenes.ToDictionary();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            OpenScene(SceneType.Game);
        }
    }

    public void OpenScene(SceneType st)
    {
        SceneManager.LoadScene(sceneDic[st].name);
    } 
}
