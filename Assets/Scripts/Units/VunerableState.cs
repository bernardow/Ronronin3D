using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Units;
using UnityEngine;
using Utilities;

public class VulnerableState : MonoBehaviour
{
    [SerializeField] private float _timer;
    private IBoss _boss;
    private SpecialUnit _baseUnit;

    private void Awake()
    {
        _boss = GameObject.FindWithTag("Boss").GetComponent<IBoss>();
        _baseUnit = GetComponent<SpecialUnit>();
    }

    public void RunVulnerableState()
    {
        _boss.StopStateMachine();
        StartCoroutine(SetVulnerableState(_timer));
    }

    private IEnumerator SetVulnerableState(float timer)
    {
        _baseUnit.DamageMultiplier = 2;
        yield return new WaitForSeconds(timer);
        _baseUnit.DamageMultiplier = 1;
        _boss.RunStateMachine();
    }
}
