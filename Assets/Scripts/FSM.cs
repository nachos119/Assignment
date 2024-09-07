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
        TransitionToState(IdleState); // 초기 상태 설정
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
        Debug.Log($"캐릭터 앞 {playerRigidbody.transform.forward}");
        Debug.Log($"카메라 앞 {Camera.main.transform.forward}");

        // Vertical과 Horizontal 값에 따라 이동 방향 계산
        Vector3 moveDirection = new Vector3(playerInput.rotate, 0, playerInput.move);

        // 이동 벡터의 크기가 0이 아니면 회전 및 이동
        if (moveDirection.magnitude > 0.1f)
        {
            // 입력된 방향으로 캐릭터를 회전시킴
            RotateTowardsMoveDirection(moveDirection);

            // 이동 거리 계산 후 적용
            Vector3 moveDistance = moveDirection.normalized * moveSpeed * Time.deltaTime;
            playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
        }
    }

    private void RotateTowardsMoveDirection(Vector3 _moveDirection)
    {
        // 현재 캐릭터가 바라보고 있는 방향을 쿼터니언으로 가져옴
        Quaternion currentRotation = playerRigidbody.rotation;

        // 이동 방향으로의 목표 회전 값 계산 (입력 방향으로 캐릭터가 회전)
        Quaternion targetRotation = Quaternion.LookRotation(_moveDirection);

        // 현재 회전과 목표 회전 사이의 각도 계산
        float angleDifference = Quaternion.Angle(currentRotation, targetRotation);

        // 최대 회전 각도 제한 (180도 이상 회전하지 않도록 제한)
        float maxRotationAngle = Mathf.Min(rotateSpeed * Time.deltaTime * 2f, angleDifference);

        // 목표 회전 값을 Slerp로 계산하여 부드럽게 회전하도록 함
        playerRigidbody.rotation = Quaternion.Slerp(currentRotation, targetRotation, maxRotationAngle / angleDifference);
    }
}
