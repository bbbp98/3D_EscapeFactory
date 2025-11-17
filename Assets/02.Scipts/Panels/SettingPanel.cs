using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        UpdateSettingPanel();
    }

    void UpdateSettingPanel()
    {
        UpdateVolumeSliders();
    }

    private void UpdateVolumeSliders()
    {
        bgmSlider.value = SoundManager.Instance.bgmVolume;
        sfxSlider.value = SoundManager.Instance.sfxVolume;
    }
    

}
