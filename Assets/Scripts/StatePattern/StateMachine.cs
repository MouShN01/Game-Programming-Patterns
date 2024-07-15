using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StateMachine
{
    public IState CurrentState { get; private set; }

    public IdleState idleState;
    public WalkState walkState;
    public JumpState jumpState;

    public event Action<IState> stateChanged;

    public StateMachine(PlayerController player)
    {
        this.idleState = new IdleState(player);
        this.walkState = new WalkState(player);
        this.jumpState = new JumpState(player);
    }

    public void Initialize(IState state)
    {
        CurrentState = state;
        state.Enter();
        stateChanged?.Invoke(state);
    }

    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();
        stateChanged?.Invoke(nextState);
    }

    public void Update()
    {
        if(CurrentState != null) 
            CurrentState.Update();
    }
}
