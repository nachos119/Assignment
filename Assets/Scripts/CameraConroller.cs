using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConroller : MonoBehaviour
{
    public Transform target;//Player

    [SerializeField] private float rotSensitive = 3f;   // 회전 감도
    [SerializeField] private float distance = 4f;       // 카메라와 타겟 사이 거리
    [SerializeField] private float smoothTime = 0.12f;  // 부드러운 회전 시간
    [SerializeField] private float heightOffset = 1.5f; // 카메라의 Y축 높이 조정

    private Vector3 currentRotation;   // 현재 회전값
    private Vector3 smoothVelocity;    // 부드러운 이동 속도 계산용

    private float yAxis;               // Y축 (좌우 회전)

    private PlayerManager playerManager = null;

    private void Awake()
    {
        playerManager = PlayerManager.Instance;
    }

    void LateUpdate()
    {
        // 마우스 우클릭이 눌려 있을 때만 좌우 회전 적용
        if (Input.GetMouseButton(1) && !playerManager.SetOtherAction) // 마우스 우클릭(1번 버튼)
        {
            // 마우스 좌우 움직임으로 Y축 회전 값 조정
            yAxis += Input.GetAxis("Mouse X") * rotSensitive;
        }

        // 목표 회전값 계산 (상하 회전은 제거, 좌우 회전만 적용)
        Vector3 targetRotation = new Vector3(0, yAxis); // 상하 회전 제거

        // 부드럽게 회전
        currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref smoothVelocity, smoothTime);
        transform.eulerAngles = currentRotation;

        // 카메라의 위치 업데이트 (타겟 위치에서 일정 거리 유지, Y축으로 높이 조정)
        Vector3 targetPosition = target.position - transform.forward * distance;
        targetPosition.y += heightOffset;  // Y축 높이 조정

        transform.position = targetPosition;
    }
}
