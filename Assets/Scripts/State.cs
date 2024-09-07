using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
    public void EnterState(FSM player);
    public void UpdateState(FSM player);
    public void ExitState(FSM player);
}

public class IdleState : IPlayerState
{
    public void EnterState(FSM player)
    {
        player.ChangeAnimation(EPlayer_State.Idle);
    }

    public void UpdateState(FSM player)
    {
        if (player.playerInput.attack)
        {
            player.TransitionToState(player.AttackState);
        }
        else if (player.playerInput.move != 0 || player.playerInput.rotate != 0)
        {
            player.TransitionToState(player.WalkState);
        }
    }

    public void ExitState(FSM player)
    {

    }
}

public class WalkState : IPlayerState
{
    public void EnterState(FSM player)
    {
        player.ChangeAnimation(EPlayer_State.Walk);
    }

    public void UpdateState(FSM player)
    {
        if (player.playerInput.attack)
        {
            player.TransitionToState(player.AttackState);
        }
        else if (player.playerInput.move == 0 && player.playerInput.rotate == 0)
        {
            player.TransitionToState(player.IdleState);
        }
    }

    public void ExitState(FSM player)
    {

    }
}



public class AttackState : IPlayerState
{
    public void EnterState(FSM player)
    {
        player.ChangeAnimation(EPlayer_State.Attack);
    }

    public void UpdateState(FSM player)
    {
        if (!player.playerInput.attack)
        {
            player.TransitionToState(player.IdleState);
        }
    }

    public void ExitState(FSM player)
    {

    }
}