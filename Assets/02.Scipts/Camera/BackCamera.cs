using UnityEngine;

public class BackCamera : MonoBehaviour
{
    public Transform target;                        //타겟

    public float distance = 4f;                     //뒤쪽으로 떨어질 거리
    public float height = 3f;                       //카메라 위치
    public float followSpeed = 8f;                  //카메라 속도

    public float speedThreshold = 5f;               //이 속도 이하라면 카메라 켜짐
    public float playerSpeed = 0f;                  //플레이어 속도

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (target == null) return;

        FollowTarget();
        UpdateCameraRotation();
        UpdateCameraVisibility();
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

    private void UpdateCameraVisibility()
    {
        cam.enabled = playerSpeed < speedThreshold;
    }

    // 외부에서 플레이어 속도 세팅
    public void SetPlayerSpeed(float speed)
    {
        playerSpeed = speed;
    }
}
