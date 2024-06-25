using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Units;
using Units.Player;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DashMinion : BaseUnit
{
    public float DashSpeed = 30;
    public bool InRange;
    public bool InRangeToAttack;
    public float DistanceTillPlayerInRange = 50;
    private Rigidbody m_Rigidbody;
    private Transform m_Transform => transform;
    private bool m_IsAtive;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(Player.Instance.PlayerTransform.position, m_Transform.position);
        InRange = distanceToPlayer < DistanceTillPlayerInRange;
        InRangeToAttack = DistanceTillPlayerInRange <= 20;

        if (InRange && !m_IsAtive)
            StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        m_IsAtive = true;
        while (m_IsAtive && InRange)
        {
            Vector3 direction = Vector3.Normalize(Player.Instance.PlayerTransform.position - m_Transform.position);
            if (!InRangeToAttack)
            {
                yield return new WaitForSeconds(1f);
                m_Rigidbody.AddForce(direction * (DashSpeed * 0.33f), ForceMode.Impulse);
            }
            else
            {
                yield return new WaitForSeconds(2f);
                m_Rigidbody.AddForce(direction * DashSpeed, ForceMode.Impulse);
            }
        }

        m_IsAtive = false;
    }
}
