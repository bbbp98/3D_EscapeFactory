using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class PetRotator : MonoBehaviour
{
    public Animator animator;
    public float interval = 6f;
    private float timer = 0f;
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= interval)
        {
            animator.SetTrigger("DoRotate");
            timer = 0f;
        }
    }
    void Start()
    {
        animator.SetTrigger("DoRotate");
    }
}
