using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private SceneAsset scene;

    public void StartGameFlow()
    {
        _SceneManager.Instance.LoadGame();
    }
}
