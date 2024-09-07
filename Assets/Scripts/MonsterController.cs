using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public IMonsterState currentState;
    public MonsterIdleState IdleState = new MonsterIdleState();
    public MonsterChaseState ChaseState = new MonsterChaseState();
    public MonsterAttackState AttackState = new MonsterAttackState();

    public float monsterMoveSpeed = 3f;
    public float monsterRotateSpeed = 180f;
    public float monsterDetectionRadius = 5f;
    public float monsterAttackRange = 2f;
    public Transform target;
    public Animator animator;

    private void Start()
    {
        TransitionToState(IdleState);
    }

    private void FixedUpdate()
    {
        currentState.UpdateState(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
        }
    }

    public void TransitionToState(IMonsterState _newState)
    {
        if (currentState != null)
        {
            currentState.ExitState(this);
        }

        currentState = _newState;
        currentState.EnterState(this);
    }

    public void ChangeAni(EPlayer_State _playerState)
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isWalk", false);
        animator.SetBool("isAttack", false);
        animator.SetBool("isDamage", false);
        animator.SetBool("isDie", false);

        switch (_playerState)
        {
            case EPlayer_State.Idle:
                animator.SetBool("isIdle", true);
                animator.SetFloat("Blend", 0.0f);
                break;
            case EPlayer_State.Walk:
                animator.SetFloat("Blend", 1.0f);
                break;
            case EPlayer_State.Attack:
                animator.SetBool("isAttack", true);
                break;
            case EPlayer_State.Damage:
                animator.SetBool("isDamage", true);
                break;
            case EPlayer_State.Die:
                animator.SetBool("isDie", true);
                break;
        }
    }
}

public interface IMonsterState
{
    void EnterState(MonsterController monster);
    void UpdateState(MonsterController monster);
    void ExitState(MonsterController monster);
}

// Idle 상태 클래스
public class MonsterIdleState : IMonsterState
{
    public void EnterState(MonsterController monster)
    {
        monster.ChangeAni(EPlayer_State.Idle);
    }

    public void UpdateState(MonsterController monster)
    {
        if (monster.target != null)
        {
            monster.TransitionToState(monster.ChaseState);
        }
    }

    public void ExitState(MonsterController monster)
    {
    }
}

public class MonsterChaseState : IMonsterState
{
    public void EnterState(MonsterController monster)
    {
        monster.ChangeAni(EPlayer_State.Walk);
    }

    public void UpdateState(MonsterController monster)
    {
        if (monster.target != null)
        {
            Vector3 direction = (monster.target.position - monster.transform.position).normalized;

            monster.transform.position += direction * monster.monsterMoveSpeed * Time.deltaTime;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                monster.transform.rotation = Quaternion.Slerp(
                    monster.transform.rotation, targetRotation, monster.monsterRotateSpeed * Time.deltaTime);
            }

            float distanceToTarget = Vector3.Distance(monster.transform.position, monster.target.position);
            if (distanceToTarget <= monster.monsterAttackRange)
            {
                monster.TransitionToState(monster.AttackState);
            }
            else if (monster.monsterDetectionRadius < distanceToTarget)
            {
                monster.target = null;
                monster.TransitionToState(monster.IdleState);
            }
        }
        else
        {
            monster.TransitionToState(monster.IdleState);
        }
    }

    public void ExitState(MonsterController monster)
    {
    }
}

public class MonsterAttackState : IMonsterState
{
    public void EnterState(MonsterController monster)
    {
        monster.ChangeAni(EPlayer_State.Attack);
    }

    public void UpdateState(MonsterController monster)
    {
        float distanceToTarget = Vector3.Distance(monster.transform.position, monster.target.position);
        if (distanceToTarget > monster.monsterAttackRange)
        {
            monster.TransitionToState(monster.ChaseState);
        }
    }

    public void ExitState(MonsterController monster)
    {
    }
}