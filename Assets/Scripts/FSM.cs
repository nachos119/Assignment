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
        TransitionToState(IdleState);
    }

    private void FixedUpdate()
    {
        currentState.UpdateState(this);

        Move();
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

    public void ChangeAnimation(EPlayer_State _newState)
    {
        playerAnimationController.ChangeAni(_newState);
    }

    private void Move()
    {
        Vector3 moveDirection = new Vector3(playerInput.rotate, 0, playerInput.move);

        if (moveDirection.magnitude > 0.1f)
        {
            Rotate(moveDirection);

            Vector3 moveDistance = moveDirection.normalized * moveSpeed * Time.deltaTime;
            playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
        }
    }

    private void Rotate(Vector3 _moveDirection)
    {
        Quaternion currentRotation = playerRigidbody.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(_moveDirection);

        float angleDifference = Quaternion.Angle(currentRotation, targetRotation);
        float maxRotationAngle = Mathf.Min(rotateSpeed * Time.deltaTime * 2f, angleDifference);

        playerRigidbody.rotation = Quaternion.Slerp(currentRotation, targetRotation, maxRotationAngle / angleDifference);
    }
}
