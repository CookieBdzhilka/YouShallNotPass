using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public State CurrentState { get; private set; }
    public void Initialize(State StartState)
    {
        CurrentState = StartState;
        CurrentState.Enter();
    }

    public void ChangeState(State NewState)
    {
        CurrentState.Exit();
        CurrentState = NewState;
        CurrentState.Enter();
    }
}
