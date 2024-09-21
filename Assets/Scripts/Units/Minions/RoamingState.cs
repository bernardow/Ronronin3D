using System;
using System.Collections;
using System.Collections.Generic;
using Units.Player;
using UnityEngine;

public class RoamingState : State
{
    [SerializeField] private float range = 5;
    private MinionsStateMachine stateMachine;

    private bool inUse;
    private float frameInterval = 10;
    private float frameCount;

    private void Awake()
    {
        stateMachine = GetComponent<MinionsStateMachine>();
    }

    public override void Enter()
    {
        frameCount = 0;
        inUse = true;
    }

    public override void Exit()
    {
        frameCount = 0;
        inUse = false;
    }

    public override void DoAction()
    {
        throw new System.NotImplementedException();
    }

    private void Update()
    {
        if (!inUse) return;

        frameCount++;
        if (frameCount % frameInterval != 0) return;
        
        LookAround();
    }

    private void LookAround()
    {
        float distance = Vector3.Distance(Player.Instance.PlayerTransform.position, transform.position);
        if (distance <= range)
            stateMachine.ChangeState(MinionsStates.ATTACKING);
    }
    private void WalkAround()
    {
        
    }
}
