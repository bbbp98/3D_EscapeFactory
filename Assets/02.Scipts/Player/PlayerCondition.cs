using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public int health;
    public PlayerController playerController;
    public AnimationHandler animationHandler;
    public GameObject monster;
    void Start()
    {
        health = 2;
        playerController = GetComponent<PlayerController>();
        animationHandler = GetComponent<AnimationHandler>();
    }

    public void Slow(float multiplier = 2f, float duration = 3f) //속도 줄이기(배율, 지속시간)
    {
        playerController.ChangeSpeedTemporaily(multiplier, duration);
    }
    public void InstantDeath() //즉사
    {
        health = 0;
        Die();
    }
    public void Damaged()
    {
        health -= 1;
        if (health <= 0)
        {
            Die();
            return;
        }
        animationHandler.DamagedAnimation();
        StartCoroutine(MoveMonsterForward(new Vector3(0, 0, -5f), 1.5f));//몬스터 서서히 이동
    }
    private IEnumerator MoveMonsterForward(Vector3 targetLocalPos, float duration)
    {
        Vector3 startPos = monster.transform.localPosition;
        float _elapsed = 0f;
        while (_elapsed < duration)
        {
            _elapsed += Time.deltaTime;
            float t = _elapsed / duration;
            monster.transform.localPosition = Vector3.Lerp(startPos, targetLocalPos, t);
            yield return null;
        }
    }
    public void Die()
    {
        StartCoroutine(DieCoroutine());
    }
    private IEnumerator DieCoroutine()
    {
        animationHandler.DieAnimation();
        yield return new WaitForSeconds(1f);
        playerController.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("충돌 감지됨"+other.name);
        if (other.CompareTag("Object"))
        {
            Debug.Log("object 태그 맞음");
            Damaged();
        }   
    }
}
