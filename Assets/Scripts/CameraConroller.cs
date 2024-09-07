using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConroller : MonoBehaviour
{
    public Transform target;//Player

    [SerializeField] private float rotSensitive = 3f;   // ȸ�� ����
    [SerializeField] private float distance = 4f;       // ī�޶�� Ÿ�� ���� �Ÿ�
    [SerializeField] private float smoothTime = 0.12f;  // �ε巯�� ȸ�� �ð�
    [SerializeField] private float heightOffset = 1.5f; // ī�޶��� Y�� ���� ����

    private Vector3 currentRotation;   // ���� ȸ����
    private Vector3 smoothVelocity;    // �ε巯�� �̵� �ӵ� ����

    private float yAxis;               // Y�� (�¿� ȸ��)

    private PlayerManager playerManager = null;

    private void Awake()
    {
        playerManager = PlayerManager.Instance;
    }

    void LateUpdate()
    {
        // ���콺 ��Ŭ���� ���� ���� ���� �¿� ȸ�� ����
        if (Input.GetMouseButton(1) && !playerManager.SetOtherAction) // ���콺 ��Ŭ��(1�� ��ư)
        {
            // ���콺 �¿� ���������� Y�� ȸ�� �� ����
            yAxis += Input.GetAxis("Mouse X") * rotSensitive;
        }

        // ��ǥ ȸ���� ��� (���� ȸ���� ����, �¿� ȸ���� ����)
        Vector3 targetRotation = new Vector3(0, yAxis); // ���� ȸ�� ����

        // �ε巴�� ȸ��
        currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref smoothVelocity, smoothTime);
        transform.eulerAngles = currentRotation;

        // ī�޶��� ��ġ ������Ʈ (Ÿ�� ��ġ���� ���� �Ÿ� ����, Y������ ���� ����)
        Vector3 targetPosition = target.position - transform.forward * distance;
        targetPosition.y += heightOffset;  // Y�� ���� ����

        transform.position = targetPosition;
    }
}
