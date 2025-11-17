using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target;                        //타겟

    public Vector3 offset = new Vector3(0, 6, -10);     //카메라 오프셋(x, y, z 축)
    public float followSpeed = 8f;                      //카메라 속도

    public float laneDistance = 2.73f;                  //레인거리
    public int laneIndex = 0;                           //플레이어가 어디 레인인지 표시

    public float tiltAmount = 6f;
    public float tiltSpeed = 5f;

    private void LateUpdate()
    {
        if (target != null) return;

        Vector3 basePos = target.position + offset;

        float laneX = laneIndex * laneDistance;
        basePos.x = laneX;

        transform.position = Vector3.Lerp
            (
            transform.position, basePos, followSpeed * Time.deltaTime
            );

        float targetTilt = laneIndex * -tiltAmount;
        Quaternion tiltRot = Quaternion.Euler(0, 0, targetTilt);

        Quaternion lookRot = Quaternion.LookRotation(
            (target.position + Vector3.forward * 10f) - transform.position
        );

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            tiltRot * lookRot,
            tiltSpeed * Time.deltaTime);
    }
}
