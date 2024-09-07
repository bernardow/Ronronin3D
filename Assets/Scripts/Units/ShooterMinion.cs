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
    private bool InRange;
    private bool IsActive;
    
    public GameObject Bomb;
    public float Force = 10;
    
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
                Rigidbody bombRigidbody = Instantiate(Bomb, transform.position, Quaternion.identity).GetComponent<Rigidbody>(); 
                bombRigidbody.AddForce((Player.Instance.PlayerTransform.position - transform.position).normalized * Force + (Vector3.up * Force), ForceMode.Impulse);
                
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(3);
        }

        IsActive = false;
    }
}
