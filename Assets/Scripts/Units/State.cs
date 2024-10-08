using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public MinionsStates StateType;
    
    public abstract void Enter();
    public abstract void Exit();
    
    public abstract void DoAction();
}
