using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public int health;
    public PlayerController playerController;
    public AnimationHandler animationHandler;

    void Start()
    {
        health = 2;
        playerController = GetComponent<PlayerController>();
        animationHandler = GetComponent<AnimationHandler>();
    }

    public void Slow(float multiplier = 2f,float duration = 3f) //속도 줄이기(배율, 지속시간)
    {
        playerController.ChangeSpeedTemporaily(multiplier,duration);
    }
    public void InstantDeath() //즉사
    {
        health = 0;
        Die();
    }
    public void Damaged()
    {
        health -= 1;
        if(health <= 0)
        {
            Die();
            return;
        }
        animationHandler.DamagedAnimation();
    }
    public void Die()
    {
        animationHandler.DieAnimation();
        Debug.Log("죽음");
        //죽기
    }

    /*private void OnTriggerEnter(Collider other)
    {
        Debug.Log("충돌 감지됨"+other.name);
        if (other.CompareTag("Object"))
        {
            Debug.Log("object 태그 맞음");
            Damaged();
        }   
    }*/
}
