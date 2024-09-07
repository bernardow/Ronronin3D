using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionsStateMachine : MonoBehaviour
{
    
    
    public State CurrentState;

    public void ChangeState(MinionsStates newState)
    {
        
    }
}

public enum MinionsStates
{
    ROAMING,
    ESCAPING,
    ATTACKING
}
