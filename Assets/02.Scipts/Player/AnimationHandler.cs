using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator anim;
    private CapsuleCollider col;

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
    }
    public void JumpAnimation()
    {
        anim.SetTrigger("Jump");
    }
    public void NotJumpAnimation()
    {
        anim.SetTrigger("Landed");
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
}
