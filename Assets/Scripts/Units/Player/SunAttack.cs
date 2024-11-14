using System;
using System.Collections;
using System.Collections.Generic;
using Units.Player;
using UnityEngine;

public class SunAttack : MonoBehaviour
{
    [SerializeField] private GameObject sunPrefab;
    [SerializeField] private float shootForce = 220;
    [SerializeField] private float staminaCost = 65;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        Player.Instance.PlayerInputs.OnBasicAttack += ShootSun;
    }

    private void OnDisable()
    {
        Player.Instance.PlayerInputs.OnBasicAttack -= ShootSun;
    }

    private void ShootSun()
    {
        if (PlayerStamina.Instance.Stamina < staminaCost) return;
        
        PlayerStamina.Instance.RemoveStamina(staminaCost);
        Rigidbody sun = Instantiate(sunPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        sun.transform.SetParent(transform.parent);
        Vector3 direction = Player.Instance.PlayerInputs.SecondatyJoystickDirectionAligned;
        direction = direction == Vector3.zero ? Player.Instance.transform.forward : direction;
        sun.AddForce(direction.normalized * shootForce, ForceMode.Impulse);
    }
}
