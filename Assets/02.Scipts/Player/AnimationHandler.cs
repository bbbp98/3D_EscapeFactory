using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator anim;
    private CapsuleCollider col;
    public GameObject _gameObject;
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
    }
    public void JumpAnimation()
    {
        anim.SetTrigger("Jump");
        LandedFalseToTrue();
    }
    public void NotJumpAnimation()
    {
        anim.SetBool("Landed",true);
    }
    public void SlidingAnimation(bool state)
    {
        anim.SetBool("Sliding", state);
        if (state)
        {
            col.height = 1.1f;
            col.center = new Vector3(0f, 0.5f, 0f);
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
}
