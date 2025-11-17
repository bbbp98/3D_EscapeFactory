using System.Collections;
using System.Collections.Generic;
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

    public void SlowAndDamage(float speed,float duration)
    {
        playerController.ChangeSpeedTemporaily(speed,duration);
        health -= 1;
        if(health <= 0)
        {
            Die();
        }
    }
    public void InstantDeath()
    {
        health = 0;
        Die();
    }
    public void Die()
    {
        //ав╠Б
    }
}
