using System;
using System.Collections;
using System.Collections.Generic;
using Units.Player;
using Unity.Mathematics;
using UnityEngine;

public class BlackHoleAttack : MonoBehaviour
{
    [SerializeField] private GameObject blackHolePrefab;
    [SerializeField] private float shootForce;
    [SerializeField] private float staminaCost = 75;
    
    private Camera mainCamera;
    
    private void Start()
    {
        Player.Instance.PlayerInputs.OnFireSpecialAttack += Shoot;
        mainCamera = Camera.main;
    }

    private void OnDisable()
    {
        Player.Instance.PlayerInputs.OnFireSpecialAttack -= Shoot;
    }

    private void Shoot()
    {
        if (PlayerStamina.Instance.Stamina < staminaCost) return;
        
        PlayerStamina.Instance.RemoveStamina(staminaCost);
        Rigidbody blackHole = Instantiate(blackHolePrefab, transform.position, quaternion.identity).GetComponent<Rigidbody>();
        blackHole.transform.SetParent(transform.parent);
        Vector3 direction = Player.Instance.PlayerInputs.SecondatyJoystickDirectionAligned;
        direction = direction == Vector3.zero ? Player.Instance.transform.forward : direction;
        
        blackHole.AddForce(direction.normalized * shootForce, ForceMode.Impulse);
    }
}
