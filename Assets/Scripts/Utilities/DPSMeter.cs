using System;
using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;

public class DPSMeter : MonoBehaviour
{
    [SerializeField] private bool _enableDPSMeter;

    private IEnumerator Start()
    {
        BaseUnit bossUnit = GameObject.FindWithTag("Boss").GetComponent<BaseUnit>();
        
        while (_enableDPSMeter)
            yield return StartCoroutine(CalculateDPSCoroutine(bossUnit));
    }

    private IEnumerator CalculateDPSCoroutine(BaseUnit unit)
    {
        float initialLife = unit.Life;
        yield return new WaitForSeconds(1);
        float updatedLife = unit.Life;
        float dps = initialLife - updatedLife;
        Debug.Log(dps);
    }
}
