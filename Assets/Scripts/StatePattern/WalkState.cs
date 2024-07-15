using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : IState
{
    private PlayerController player;

    public WalkState(PlayerController player)
    {
        this.player = player;
    }
    
    public void Enter(){}

    public void Update()
    {
        if (!player.IsGrounded)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.jumpState);
        }

        if (Mathf.Abs(player.CharController.velocity.x) < 0.1f && Mathf.Abs(player.CharController.velocity.z) < 0.1f)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.idleState);
        }
    }

    public void Exit(){}
}
