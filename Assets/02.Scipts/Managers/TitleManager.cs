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
        SoundManager.Instance.RestartBgmAudio(BgmSounds.Main);
    }

    public void StartGameFlow()
    {
        _SceneManager.Instance.LoadGame();
        SoundManager.Instance.OnOffClickAudio(ClickSounds.Click, true);
    }

    public void OpenPetUI()
    {
        UIManager.Instance.CallUIOnOff(PanelType.Pet, true);
    }

}
