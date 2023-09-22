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
            Vector3 goalPos = (new Vector3(_player.PlayerInputs.Horizontal, _player.PlayerTransform.localPosition.y, //todo fix
                _player.PlayerInputs.Vertical) + _player.PlayerTransform.localPosition).normalized * _dashImpulse;
            goalPos.CheckForOutScreen(22, -20, 17.7f, -8.7f);
            _player.PlayerTransform.DOMove(goalPos, 0.5f).SetEase(Ease.Linear);
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
