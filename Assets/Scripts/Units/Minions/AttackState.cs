using System;
using System.Collections;
using System.Collections.Generic;
using Units;
using Units.Player;
using UnityEngine;

public class AttackState : State
{
    private MinionsStateMachine stateMachine;
    private BaseUnit baseUnit;
    private IAttack attack;

    [SerializeField] private float intervalBetweenAttacks = 3;

    private bool inUse;
    
    private void Awake()
    {
        attack = GetComponent<IAttack>();
        baseUnit = GetComponent<BaseUnit>();
        stateMachine = GetComponent<MinionsStateMachine>();
    }

    public override void Enter()
    {
        inUse = true;
        StartCoroutine(AttackCoroutine());
    }

    public override void Exit()
    {
        inUse = false;
    }

    public override void DoAction()
    {
        throw new System.NotImplementedException();
    }

    private IEnumerator AttackCoroutine()
    {
        while (inUse)
        {
            if (Vector3.Distance(transform.position, Player.Instance.PlayerTransform.position) > 20)
            {
                stateMachine.ChangeState(MinionsStates.ROAMING);
                yield break;
            }
            
            attack.Attack();
            
            yield return new WaitForSeconds(intervalBetweenAttacks);
        }
    }
}
