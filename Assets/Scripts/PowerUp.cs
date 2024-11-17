using System;
using System.Collections;
using System.Collections.Generic;
using Units.Player;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType
    {
        BLACK_HOLE,
        SUN,
        DASH
    }

    [SerializeField] private PowerUpType type;
    [SerializeField] private ShowUpgradeFeedback PowerUpFeedback;

    private void Update()
    {
        transform.parent.Rotate(new Vector3(0.5f, 0.5f, 0.5f), 10 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        if (type == PowerUpType.SUN)
            Player.Instance.SunAttack.Unlocked = true;
        else if (type == PowerUpType.BLACK_HOLE) Player.Instance.BlackHoleAttack.Unlocked = true;
        else Player.Instance.PlayerDash.Unlocked = true;

        PowerUpFeedback.ShowFeedback(type);
        
        Destroy(transform.parent.gameObject);
    }
}
