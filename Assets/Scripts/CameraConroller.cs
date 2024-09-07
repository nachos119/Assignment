using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConroller : MonoBehaviour
{
    public Transform target;//Player

    [SerializeField] private float rotSensitive = 3f;
    [SerializeField] private float distance = 4f;
    [SerializeField] private float smoothTime = 0.12f;
    [SerializeField] private float heightOffset = 1.5f;

    private Vector3 currentRotation;
    private Vector3 smoothVelocity;

    private float yAxis;

    private PlayerManager playerManager = null;

    private void Awake()
    {
        playerManager = PlayerManager.Instance;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(1) && !playerManager.SetOtherAction)
        {
            yAxis += Input.GetAxis("Mouse X") * rotSensitive;
        }

        Vector3 targetRotation = new Vector3(0, yAxis);

        currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref smoothVelocity, smoothTime);
        transform.eulerAngles = currentRotation;

        Vector3 targetPosition = target.position - transform.forward * distance;
        targetPosition.y += heightOffset;

        transform.position = targetPosition;
    }
}
