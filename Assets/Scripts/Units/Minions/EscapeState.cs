using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EscapeState : State
{
    private MinionsStateMachine stateMachine;
    private Vector3[] rayDirections;

    private void Awake()
    {
        stateMachine = GetComponent<MinionsStateMachine>();
        
        Vector3 right = transform.right;
        Vector3 left = -right;
        Vector3 forward = transform.forward;
        Vector3 back = -forward;
        Vector3 leftForward = left + forward;
        Vector3 rightForward = right + forward;
        Vector3 leftBack = left + back;
        Vector3 rightBack = right + back;
        
        rayDirections = new [] { left.normalized, right.normalized, forward.normalized, back.normalized, 
            leftForward.normalized, rightForward.normalized, leftBack.normalized, rightBack.normalized};
    }

    public override void Enter()
    {
        Escape();
    }

    public override void Exit()
    {
    }

    public override void DoAction()
    {
        throw new System.NotImplementedException();
    }

    private void Escape()
    {
        KeyValuePair<Vector3, float> betterRoute = LookForBetterRoute();

        transform.position += betterRoute.Key * (betterRoute.Value - 2);
        
        stateMachine.ChangeState(MinionsStates.ROAMING);
    }

    private KeyValuePair<Vector3, float> LookForBetterRoute()
    {
        Dictionary<Vector3, float> directionsDistances = new Dictionary<Vector3, float>();
        
        for (int i = 0; i < rayDirections.Length; i++)
        {
            if (Physics.Raycast(transform.position, rayDirections[i], out RaycastHit hit))
            {
                directionsDistances.Add(rayDirections[i], Vector3.Distance(transform.position, hit.transform.position));
            }
        }

        Vector3 betterDirection = new Vector3();
        foreach (KeyValuePair<Vector3, float> direction in directionsDistances)
        {
            if (betterDirection == new Vector3())
            {
                betterDirection = direction.Key;
                continue;
            }

            if (direction.Value < directionsDistances[betterDirection])
            {
                betterDirection = direction.Key;
            }
        }

        return new KeyValuePair<Vector3, float>(betterDirection, directionsDistances[betterDirection]);
    }
}
