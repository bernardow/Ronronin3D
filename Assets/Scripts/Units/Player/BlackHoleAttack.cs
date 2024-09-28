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
        Rigidbody blackHole = Instantiate(blackHolePrefab, transform.position, quaternion.identity).GetComponent<Rigidbody>();
        Vector3 direction = (ShootRaycast() - Player.Instance.PlayerTransform.position).normalized;
        direction.y = 0;
        blackHole.AddForce(direction * shootForce, ForceMode.Impulse);
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
