using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    // 인스펙터 전용 커스텀 사전
    public SerializableDictionary<BgmSounds, AudioClip> bgmSounds;
    public SerializableDictionary<UISounds, AudioClip> uiSounds;
    public SerializableDictionary<ClickSounds, AudioClip> clickSounds;

    // 실제 사용 사전
    private Dictionary<BgmSounds, AudioClip> bgmSoundDic = new Dictionary<BgmSounds, AudioClip>();
    private Dictionary<UISounds, AudioClip> uiSoundDic = new Dictionary<UISounds, AudioClip>();
    private Dictionary<ClickSounds, AudioClip> clickSoundDic = new Dictionary<ClickSounds, AudioClip>();

    // 오디오 소스 리스트, 인덱스
    [SerializeField]private List<AudioSource> bgmSources;
    private int bgmIndex = 0;
    [SerializeField] private List<AudioSource> clickSources;
    private int clickIndex = 0;
    [SerializeField] private List<AudioSource> uiSources;
    private int uiIndex = 0;

    [Header("Sound Options")]
    public float fadeTime;

    public float bgmVolume;

    public float sfxVolume;

    void Awake()
    {
        // 싱글톤
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        bgmSoundDic = bgmSounds.ToDictionary();
        uiSoundDic = uiSounds.ToDictionary();
        clickSoundDic = clickSounds.ToDictionary();

        LoadVolumes();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            OnOffBgmAudio(BgmSounds.Main, true);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            OnOffBgmAudio(BgmSounds.Main, false);
        }
    }


    public void ChangeBgmVolume(float v)
    {
        bgmVolume = v;

        foreach(AudioSource audioSource in bgmSources)
        {
            audioSource.volume = v;
        }

        // Playerprefs에 저장 필요
        SaveBgmVolume();
    }

    public void ChangeSfxVolume(float v)
    {
        sfxVolume = v;

        foreach(AudioSource audioSource in uiSources)
        {
            audioSource.volume = v;
        }

        foreach(AudioSource audioSource in clickSources)
        {
            audioSource.volume = v;
        }

        // Playerprefs에 저장 필요
        SaveSfxVolume();
    }
    
    private void SaveBgmVolume()
    {
        PlayerPrefs.SetFloat("BgmVolume", bgmVolume);
    }

    private void SaveSfxVolume()
    {
        PlayerPrefs.SetFloat("SfxVolume", sfxVolume);
    }

    public void LoadVolumes()
    {
        ChangeBgmVolume(PlayerPrefs.GetFloat("BgmVolume", 1f));
        ChangeSfxVolume(PlayerPrefs.GetFloat("SfxVolume", 1f));
    }

    public void OnOffBgmAudio(BgmSounds bs, bool isOn)
    {
        if (isOn)
        {
            var bgm = bgmSources[bgmIndex];
            bgm.clip = bgmSoundDic[bs];
            bgmIndex = (bgmIndex + 1) % bgmSources.Count;

            bgm.Play();
            StartCoroutine(FadeIn(bgm));
        }
        else
        {
            for(int i = bgmSources.Count - 1; i >= 0; i--)
            {
                if(bgmSources[i].clip == bgmSoundDic[bs])
                {
                    StartCoroutine(FadeOut(bgmSources[i]));
                    StartCoroutine(StopAudio(bgmSources[i]));
                }
            }
        }
    }

    public void OnOffUiAudio(UISounds us, bool isOn)
    {
        if (isOn)
        {
            var ui = uiSources[uiIndex];
            ui.clip = uiSoundDic[us];
            uiIndex = (uiIndex + 1) % uiSources.Count;

            ui.Play();
            //StartCoroutine(FadeIn(ui));
        }
        else
        {
            for(int i = uiSources.Count - 1; i >= 0; i--)
            {
                if(uiSources[i].clip == uiSoundDic[us])
                {
                    uiSources[i].Stop();
                }
            }
        }
    }

    public void OnOffClickAudio(ClickSounds cs, bool isOn)
    {
        if (isOn)
        {
            var click = clickSources[clickIndex];
            click.clip = clickSoundDic[cs];
            clickIndex = (clickIndex + 1) % clickSources.Count;

            click.Play();
            //StartCoroutine(FadeIn(click));
        }
        else
        {
            for(int i = clickSources.Count - 1; i >= 0; i--)
            {
                if(clickSources[i].clip == clickSoundDic[cs])
                {
                    clickSources[i].Stop();
                }
            }
        }
    }

    IEnumerator FadeIn(AudioSource audioSource)
    {
        float t = 0;

        if(audioSource != null)
        {
            audioSource.Play();
            while(t < fadeTime)
            {
                t += Time.fixedDeltaTime;
                audioSource.volume = Mathf.Lerp(0, bgmVolume, t/fadeTime);

                yield return new WaitForFixedUpdate();          
            }
        }
    }

    IEnumerator FadeOut(AudioSource audioSource)
    {
        float t = 0;

        if(audioSource != null)
        {
            while(t < fadeTime)
            {
                t += Time.fixedDeltaTime;
                audioSource.volume = Mathf.Lerp(bgmVolume, 0, t/fadeTime);

                yield return new WaitForFixedUpdate();         
            }
        }
        audioSource.Stop();
    }

    IEnumerator StopAudio(AudioSource audioSource)
    {
        yield return new WaitForSeconds(fadeTime);

        audioSource.clip = null;

        yield return null;
    }
}
