using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    public Animator animator { get; private set; }
    public PlayerController playerInput { get; private set; }
    private Rigidbody playerRigidbody;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 180f;

    public IPlayerState currentState { get; private set; }

    public IdleState IdleState = new IdleState();
    public WalkState WalkState = new WalkState();
    public AttackState AttackState = new AttackState();
    // public DamageState DamageState = new DamageState();
    // public DieState DieState = new DieState();

    public PlayerAnimationController playerAnimationController = null;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerController>();
        playerRigidbody = GetComponent<Rigidbody>();

        playerAnimationController = GetComponent<PlayerAnimationController>();
    }


    private void Start()
    {
        TransitionToState(IdleState); // �ʱ� ���� ����
    }

    private void FixedUpdate()
    {
        currentState.UpdateState(this);

        // Move();
        // Rotate();

        MoveAndRotate();
    }

    public void TransitionToState(IPlayerState _newState)
    {
        if (currentState != null)
        {
            currentState.ExitState(this);
        }

        currentState = _newState;
        currentState.EnterState(this);
    }

    private void Move()
    {
        Vector3 moveDistance = playerInput.move * transform.forward * moveSpeed * Time.deltaTime;

        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
    }

    private void Rotate()
    {
        float turn = playerInput.rotate * rotateSpeed * Time.deltaTime;
        playerRigidbody.rotation = playerRigidbody.rotation * Quaternion.Euler(0, turn, 0f);
    }

    public void ChangeAnimation(EPlayer_State _newState)
    {
        playerAnimationController.ChangeAni(_newState);
    }

    private void MoveAndRotate()
    {
        Debug.Log($"ĳ���� �� {playerRigidbody.transform.forward}");
        Debug.Log($"ī�޶� �� {Camera.main.transform.forward}");

        // Vertical�� Horizontal ���� ���� �̵� ���� ���
        Vector3 moveDirection = new Vector3(playerInput.rotate, 0, playerInput.move);

        // �̵� ������ ũ�Ⱑ 0�� �ƴϸ� ȸ�� �� �̵�
        if (moveDirection.magnitude > 0.1f)
        {
            // �Էµ� �������� ĳ���͸� ȸ����Ŵ
            RotateTowardsMoveDirection(moveDirection);

            // �̵� �Ÿ� ��� �� ����
            Vector3 moveDistance = moveDirection.normalized * moveSpeed * Time.deltaTime;
            playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
        }
    }

    private void RotateTowardsMoveDirection(Vector3 _moveDirection)
    {
        // ���� ĳ���Ͱ� �ٶ󺸰� �ִ� ������ ���ʹϾ����� ������
        Quaternion currentRotation = playerRigidbody.rotation;

        // �̵� ���������� ��ǥ ȸ�� �� ��� (�Է� �������� ĳ���Ͱ� ȸ��)
        Quaternion targetRotation = Quaternion.LookRotation(_moveDirection);

        // ���� ȸ���� ��ǥ ȸ�� ������ ���� ���
        float angleDifference = Quaternion.Angle(currentRotation, targetRotation);

        // �ִ� ȸ�� ���� ���� (180�� �̻� ȸ������ �ʵ��� ����)
        float maxRotationAngle = Mathf.Min(rotateSpeed * Time.deltaTime * 2f, angleDifference);

        // ��ǥ ȸ�� ���� Slerp�� ����Ͽ� �ε巴�� ȸ���ϵ��� ��
        playerRigidbody.rotation = Quaternion.Slerp(currentRotation, targetRotation, maxRotationAngle / angleDifference);
    }
}
