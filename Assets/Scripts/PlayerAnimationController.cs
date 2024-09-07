using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator playerAnimator = null;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }

    public void ChangeAni(EPlayer_State _playerState)
    {
        playerAnimator.SetBool("isIdle", false);
        playerAnimator.SetBool("isWalk", false);
        playerAnimator.SetBool("isAttack", false);
        playerAnimator.SetBool("isDamage", false);
        playerAnimator.SetBool("isDie", false);

        switch (_playerState)
        {
            case EPlayer_State.Idle:
                playerAnimator.SetBool("isIdle", true);
                playerAnimator.SetFloat("Blend", 0.0f);
                break;
            case EPlayer_State.Walk:
                playerAnimator.SetFloat("Blend", 1.0f);
                break;
            case EPlayer_State.Attack:
                playerAnimator.SetBool("isAttack", true);
                break;
            case EPlayer_State.Damage:
                playerAnimator.SetBool("isDamage", true);
                break;
            case EPlayer_State.Die:
                playerAnimator.SetBool("isDie", true);
                break;
        }
    }
}
