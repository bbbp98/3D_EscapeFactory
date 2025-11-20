using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator anim;
    private CapsuleCollider col;
    public GameObject _gameObject;//«√∑π¿ÃæÓ ≈∏∞› ¿Ã∆Â∆Æ
    public GameObject _gameObject2;//∆Í ≈∏∞› ¿Ã∆Â∆Æ
    void Awake()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("animator is null");
        }
    }
    void Start()
    {
        col = GetComponent<CapsuleCollider>();
        anim.SetBool("IsDie", false);
        _gameObject.SetActive(false);
        _gameObject2.SetActive(false);
    }
    public void JumpAnimation()
    {
        anim.Play("Jump_Full_Long", 0, 0f);
        anim.SetBool("Landed", false);

        // play sfx
        SoundManager.Instance.OnOffClickAudio(ClickSounds.Jump, true);
    }
    public void SlidingAnimation(bool state)
    {
        anim.SetBool("Sliding", state);
        if (state)
        {
            col.height = 1.1f;
            col.center = new Vector3(0f, 0.5f, 0f);
            // play sfx
            SoundManager.Instance.OnOffClickAudio(ClickSounds.Slide, true);
        }
        else
        {
            col.height = 2.05f;
            col.center = new Vector3(0f, 1f, 0f);
        }
    }
    public void DieAnimation()
    {
        _gameObject.SetActive(false);
        ShowEffect();
        anim.SetTrigger("IsDie");

        // play sfx
        SoundManager.Instance.OnOffClickAudio(ClickSounds.GameOver, true);
    }
    public void DamagedAnimation()
    {
        anim.SetTrigger("Damaged");
        ShowEffect();
    }
    public void ShowEffect()
    {
        StartCoroutine(EffectdCoroutine());
    }
    private IEnumerator EffectdCoroutine()
    {
        _gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        _gameObject.SetActive(false);
    }
    public void ShowPetEffect()
    {
        StartCoroutine(PetEffectCoroutine());
    }
    private IEnumerator PetEffectCoroutine()
    {
        _gameObject2.SetActive(true);
        yield return new WaitForSeconds(2);
        _gameObject2.SetActive(false);
    }
    public void LandedFalseToTrue()
    {
        StartCoroutine(LandedCoroutine());
    }
    private IEnumerator LandedCoroutine()
    {
        anim.SetBool("Landed", false);
        yield return new WaitForSeconds(0.9f);
        anim.SetBool("Landed", true);
    }
    public void Landed()
    {
        anim.SetBool("Landed", true);
    }
}
