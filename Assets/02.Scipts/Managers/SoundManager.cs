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

    [Header("Sound Options")]
    public float fadeTime;

    [SerializeField] private List<AudioSource> BgmSources;
    [SerializeField] private List<AudioSource> sfxSources;
    [SerializeField] private List<AudioSource> UISources;

    void Awake()
    {
        // 싱글톤
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        bgmSoundDic = bgmSounds.ToDictionary();
        uiSoundDic = uiSounds.ToDictionary();
        clickSoundDic = clickSounds.ToDictionary();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {

        }
    }

    public void OnOffBgmAudio(BgmSounds bs, bool isOn)
    {
        if (isOn)
        {
            AudioSource bgm = new AudioSource();
            bgm.clip = bgmSoundDic[bs];
            BgmSources.Add(bgm);

            bgm.Play();
            StartCoroutine(FadeIn(bgm));
        }
        else
        {
            for(int i = BgmSources.Count - 1; i >= 0; i--)
            {
                if(BgmSources[i].clip == bgmSoundDic[bs])
                {
                    StartCoroutine(FadeOut(BgmSources[i]));
                    //BgmSources.Remove(BgmSources[i]);
                }
            }
        }
    }

    public void OnOffUiAudio(UISounds us, bool isOn)
    {
        
    }

    public void OnOffClickAudio(ClickSounds cs, bool isOn)
    {
        
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
                audioSource.volume = Mathf.Lerp(0, 1, t/fadeTime);

                yield return new WaitForFixedUpdate();          
            }
        }
    }

    IEnumerator FadeOut(AudioSource audioSource)
    {
        float t = 0;

        if(audioSource != null)
        {
            audioSource.Play();
            while(t < fadeTime)
            {
                t += Time.fixedDeltaTime;
                audioSource.volume = Mathf.Lerp(1, 0, t/fadeTime);

                yield return new WaitForFixedUpdate();         
            }
        }
        audioSource.Stop();
    }
}
