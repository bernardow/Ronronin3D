using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionsStateMachine : MonoBehaviour
{
    [SerializeField] private EscapeState escapeState;
    [SerializeField] private RoamingState roamingState;
    [SerializeField] private AttackState attackState;
    
    public State CurrentState;

    private void OnEnable()
    {
        CurrentState.Enter();
    }

    public void ChangeState(MinionsStates newState)
    {
        CurrentState.Exit();
        CurrentState = GetStateByType(newState);
        CurrentState.Enter();
    }

    private State GetStateByType(MinionsStates type)
    {
        State newState = type switch
        {
            MinionsStates.ESCAPING => escapeState,
            MinionsStates.ROAMING => roamingState,
            MinionsStates.ATTACKING => attackState,
            _ => roamingState
        };

        return newState;
    }
}