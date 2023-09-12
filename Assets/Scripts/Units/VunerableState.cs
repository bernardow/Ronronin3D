using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulnerableState : MonoBehaviour
{
    private bool _isVulnerable;

    public bool IsVulnerable { get; private set; }

    private IEnumerator TurnVulnerable()
    {
        _isVulnerable = true;
        yield return new WaitForSeconds(5);
        _isVulnerable = false;
    }
}
