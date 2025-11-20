using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public enum BuffType
{
    Magnet,
    ShieldAndBoost,
}

public class PlayerCondition : MonoBehaviour
{
    public int health;
    public PlayerController playerController;
    public AnimationHandler animationHandler;
    public GameObject monster;
    public Animator monsterAnimator;
    bool monsterForward;
    bool invincible; //무적효과
    public bool magnetActive = false;//마그네틱

    private float originSpeed;
    [SerializeField] private GameObject shieldEffectPrefab;
    private GameObject currentShield;

    private Dictionary<BuffType, Coroutine> coroutineDict = new Dictionary<BuffType, Coroutine>();

    void Start()
    {
        health = 2;
        invincible = false;
        monsterForward = false;
        playerController = GetComponent<PlayerController>();
        animationHandler = GetComponent<AnimationHandler>();
        monsterAnimator = monster.GetComponent<Animator>();

        originSpeed = playerController.moveSpeed;
    }

    public void Slow(float multiplier = 2f, float duration = 3f) //속도 줄이기(배율, 지속시간)
    {
        if (invincible) return;
        playerController.ChangeSpeedTemporaily(multiplier, duration);
    }

    public void InstantDeath() //즉사
    {
        if (health <= 0 || invincible) return;
        health = 0;
        Die();
    }

    public void Damaged()
    {
        if (health <= 0 || invincible) return;
        health -= 1;
        if (health <= 0)
        {
            Die();
            return;
        }
        animationHandler.DamagedAnimation();
        monsterForward = true;
        StartCoroutine(MoveMonsterForward(new Vector3(0, 0, -5.5f), 1.5f));//몬스터 서서히 이동
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
        playerController.moveSpeed = 0f;
        StartCoroutine(DieCoroutine(new Vector3(1.46f, 0, -2.32f), 1.5f));
    }

    private IEnumerator DieCoroutine(Vector3 targetLocalPos, float duration)
    {
        animationHandler.DieAnimation();
        Vector3 startPos = monster.transform.localPosition;
        Quaternion startRot = monster.transform.localRotation;
        Quaternion targetRot = Quaternion.Euler(0f, -28f, 0f);
        float _elapsed = 0f;
        while (_elapsed < duration)
        {
            _elapsed += Time.deltaTime;
            float t = _elapsed / duration;
            monster.transform.localPosition = Vector3.Lerp(startPos, targetLocalPos, t);
            monster.transform.localRotation = Quaternion.Lerp(startRot, targetRot, t);
            yield return null;
        }
        monsterAnimator.SetTrigger("Smash");
        yield return new WaitForSeconds(0.4f);
        playerController.enabled = false;
        yield return new WaitForSeconds(3f);
        GameManager.Instance.GameOver();
    }

    #region Item Effect
    public void ShieldAndBoost(float multiplier = 2f, float duration = 3f) //무적부스트
    {
        if (coroutineDict.ContainsKey(BuffType.ShieldAndBoost))
            StopCoroutine(coroutineDict[BuffType.ShieldAndBoost]);
        if (currentShield != null)
            Destroy(currentShield);

        currentShield = Instantiate(shieldEffectPrefab, transform);
        currentShield.transform.position += Vector3.up;
        coroutineDict[BuffType.ShieldAndBoost] = StartCoroutine(ShieldCoroutine(multiplier, duration));
    }

    private IEnumerator ShieldCoroutine(float multiplier, float duration)
    {
        playerController.moveSpeed = originSpeed * multiplier;
        invincible = true;

        yield return new WaitForSeconds(duration);

        invincible = false;
        playerController.moveSpeed = originSpeed;

        if (currentShield != null)
            Destroy(currentShield);
    }

    public void Heal()
    {
        if (health <= 0 || health == 2) return;
        health = 2;
        if (monsterForward)
        {
            monsterForward = false;
            StartCoroutine(MoveMonsterBackward(new Vector3(0, 0, -7.7f), 1.5f));
        }
    }

    private IEnumerator MoveMonsterBackward(Vector3 targetLocalPos, float duration)
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

    public void ActivateMagnet(float duration)
    {
        if (!magnetActive)
        {
            magnetActive = true;
            if (coroutineDict.ContainsKey(BuffType.Magnet))
                StopCoroutine(coroutineDict[BuffType.Magnet]);

            coroutineDict[BuffType.Magnet] = StartCoroutine(MagnetCoroutine(duration));
        }
    }

    private IEnumerator MagnetCoroutine(float duration)
    {
        ItemBase.SetMagnetTarget(playerController);
        yield return new WaitForSeconds(duration);
        magnetActive = false;
        ItemBase.ClearMagnetTarget();
    }
    #endregion
}
