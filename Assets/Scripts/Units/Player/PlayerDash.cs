using System;
using System.Collections;
using System.Collections.Generic;
using Units.Player;
using UnityEngine;
using DG.Tweening;
using Utilities;

public class PlayerDash : MonoBehaviour
{
    private Player _player;
    private bool _canDash = true;
    [SerializeField] private float _dashImpulse = 5;
    [SerializeField] private LayerMask _mask;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _player.PlayerInputs.OnDashActivate += Dash;
    }

    private void Dash()
    {
        if (_canDash)
        {
            Vector3 goalPos = _player.PlayerInputs.MovementDirectionRaw * _dashImpulse + _player.PlayerTransform.localPosition;
            goalPos = Helpers.CheckForOutScreen(goalPos, 22, -20, 17.7f, -8.7f);
            //goalPos = Helpers.CheckForInSetupCollision(goalPos, _player.PlayerTransform, _player.PlayerCollider, _mask);
            _player.PlayerTransform.DOMove(goalPos, 0.25f).SetEase(Ease.Linear);
            StartCoroutine(SetDashCooldown());
        }
    }

    private IEnumerator SetDashCooldown()
    {
        _canDash = false;
        yield return new WaitForSeconds(0.5f);
        _canDash = true;
    }
}
