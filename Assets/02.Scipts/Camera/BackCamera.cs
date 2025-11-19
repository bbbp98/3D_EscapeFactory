using UnityEngine;

public class BackCamera : MonoBehaviour
{
    public Transform target;                        //타겟

    public float distance = 2f;                     //뒤쪽으로 떨어질 거리
    public float height = 1.5f;                       //카메라 위치
    public float followSpeed = 8f;                  //카메라 속도

    public float speedThreshold = 8f;               //이 속도 이하라면 카메라 켜짐
    public float playerSpeed;                //플레이어 속도


    public GameObject mirrorUI;

    private Vector3 lastPos;

    public PlayerCondition playerCondition;

    private void Start()
    {
        lastPos = target.position;
    }

    private void LateUpdate()
    {
        if (target == null) return;


        UpdatePlayerSpeed();
        FollowTarget();
        UpdateCameraRotation();
        UpdateMirrorUI();
    }

    private void UpdatePlayerSpeed()
    {
        Vector3 move = target.position - lastPos;
        playerSpeed = Vector3.Dot(move / Time.deltaTime, target.forward); 

        lastPos = target.position;
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
        // 속도가 느리면 UI 보이기
        mirrorUI.SetActive(playerSpeed < speedThreshold);

        if (playerCondition != null && (playerCondition.health <= 0))
        {
            mirrorUI.SetActive(false);
            return;
        }
    }
}
