using System;
using System.Collections;
using System.Collections.Generic;
using Units.Player;
using UnityEngine;

public class SunAttack : MonoBehaviour
{
    [SerializeField] private GameObject sunPrefab;
    [SerializeField] private float shootForce = 220;
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
        Rigidbody sun = Instantiate(sunPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        Vector3 direction = (ShootRaycast() - Player.Instance.PlayerTransform.position).normalized;
        direction.y = 0;
        sun.AddForce(direction * shootForce, ForceMode.Impulse);
    }
    
    private Vector3 ShootRaycast()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            return hitInfo.point;
        }

        throw new NullReferenceException("Shooting out of the screen");
    }
}
