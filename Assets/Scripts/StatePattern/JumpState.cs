using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : IState
{
    private PlayerController player;

    public JumpState(PlayerController player)
    {
        this.player = player;
    }
    
    public void Enter(){}

    public void Update()
    {
        if (player.IsGrounded)
        {
            if (Mathf.Abs(player.CharController.velocity.x) > 0.1f || Mathf.Abs(player.CharController.velocity.z) > 0.1f)
            {
                player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.walkState);
            }
            else
            {
                player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.idleState);
            }
        }
    }

    public void Exit(){}
}
