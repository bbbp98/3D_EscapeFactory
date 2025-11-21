using UnityEngine;

public class BackCamera : MonoBehaviour
{
    public Transform target;                        //타겟

    public float distance = 2f;                     //뒤쪽으로 떨어질 거리
    public float height = 1.5f;                       //카메라 위치
    public float followSpeed = 8f;                  //카메라 속도

    private int lastHealth = -1;
    private float mirrorTimer = 0f;

    public GameObject mirrorUI;


    public PlayerCondition playerCondition;


    private void LateUpdate()
    {
        if (target == null) return;


        FollowTarget();
        UpdateCameraRotation();
        UpdateMirrorUI();
    }


    private void FollowTarget()
    {
        Vector3 targetPos =
            target.position
            - target.forward * distance
            + Vector3.up * height;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            followSpeed * Time.deltaTime
        );
    }

    private void UpdateCameraRotation()
    {
        transform.rotation = Quaternion.LookRotation(-target.forward, Vector3.up);
    }

    private void UpdateMirrorUI()
    {
        if (playerCondition == null) return;

        if (lastHealth == -1)
            lastHealth = playerCondition.health;

        // 체력이 줄었을 때만 체크
        if (playerCondition.health < lastHealth)
        {
            // 오직 "1일 때"만 켜짐
            if (playerCondition.health == 1)
            {
                mirrorUI.SetActive(true);
                mirrorTimer = 3f;
            }
        }

        // 0이면 항상 꺼짐
        if (playerCondition.health == 0)
        {
            mirrorUI.SetActive(false);
            mirrorTimer = 0;
        }

        // 2 이상이면 절대 켜지지 않게 확실히 막기
        if (playerCondition.health >= 2)
        {
            mirrorUI.SetActive(false);
        }

        // 타이머 작동
        if (mirrorTimer > 0)
        {
            mirrorTimer -= Time.deltaTime;
            if (mirrorTimer <= 0)
            {
                mirrorUI.SetActive(false);
            }
        }

        lastHealth = playerCondition.health;
    }
}

