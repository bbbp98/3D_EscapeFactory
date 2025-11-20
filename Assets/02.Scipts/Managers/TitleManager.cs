using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private SceneAsset scene;

    void Start()
    {
        SoundManager.Instance.OnOffBgmAudio(BgmSounds.Main, false);
        SoundManager.Instance.OnOffBgmAudio(BgmSounds.Main, true);
    }

    public void StartGameFlow()
    {
        _SceneManager.Instance.LoadGame();
        SoundManager.Instance.OnOffClickAudio(ClickSounds.Click, true);
    }

}
