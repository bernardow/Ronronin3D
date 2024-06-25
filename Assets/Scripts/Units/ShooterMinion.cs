using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Units;
using Units.Player;
using UnityEngine;

public class ShooterMinion : BaseUnit
{
    public float DistanceToPlayerThreshold = 50;
    public float SporeSpeed = 10;
    private bool InRange;
    private bool IsActive;
    
    private Transform m_Transform => transform;
    
    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(Player.Instance.PlayerTransform.position, m_Transform.position);
        InRange = distanceToPlayer < DistanceToPlayerThreshold;

        if (InRange && !IsActive)
            StartCoroutine(ShooterLogic());
    }

    private IEnumerator ShooterLogic()
    {
        while (InRange && IsAlive)
        {
            IsActive = true;
            for (int i = 0; i < 3; i++)
            {
                SporePoolUnit sporePoolUnit = SporePoolController.Instance.GetSporeInPool();
                if (sporePoolUnit != null) 
                    sporePoolUnit.SetSporeMove(m_Transform.position, (Player.Instance.PlayerTransform.position - m_Transform.position).normalized * SporeSpeed);
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(3);
        }

        IsActive = false;
    }
}
