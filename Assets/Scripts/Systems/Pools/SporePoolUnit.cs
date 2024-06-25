using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporePoolUnit : MonoBehaviour
{
    public bool InUse;
    private Rigidbody m_Rigidbody;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    public void SetSporeMove(Vector3 startPosition, Vector3 direction)
    {
        if (m_Rigidbody == null)
            m_Rigidbody = GetComponent<Rigidbody>();
        
        InUse = true;
        m_Rigidbody.position = startPosition;
        m_Rigidbody.AddForce(direction, ForceMode.Impulse);
    }

    private void OnDisable()
    {
        InUse = false;
    }
}
