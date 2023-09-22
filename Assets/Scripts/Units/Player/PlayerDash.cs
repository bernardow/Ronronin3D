using System;
using System.Collections;
using System.Collections.Generic;
using Units.Player;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private Player _player;

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
        
    }
}
