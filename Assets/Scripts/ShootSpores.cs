using System;
using System.Collections;
using System.Collections.Generic;
using Units;
using Units.Player;
using Unity.Mathematics;
using UnityEngine;

public class ShootSpores : BaseUnit, IAttack
{
    [SerializeField] private GameObject sporePrefab;
    [SerializeField] private float sporeForce = 3;
    
    public void Attack()
    {
        SpawnSpores();
    }

#if UNITY_EDITOR
    private void Update()
    {
        Vector3 myPosition = transform.position;
        Vector3 playerPosition = Player.Instance.PlayerTransform.position;
        Vector3 playerRight = Vector3.Cross(playerPosition - myPosition, Vector3.up) + playerPosition;
        Vector3 playerLeft = Vector3.Cross(playerPosition - myPosition, -Vector3.up) + playerPosition;
        
        Debug.DrawLine(playerPosition, myPosition, Color.blue);
        Debug.DrawLine(playerLeft, myPosition, Color.red);
        Debug.DrawLine(playerRight, myPosition, Color.green);
    }
#endif
    
    private void SpawnSpores()
    {
        Vector3 myPosition = transform.position;
        Vector3 playerPosition = Player.Instance.PlayerTransform.position;
        Vector3 playerRight = Vector3.Cross(playerPosition - myPosition, Vector3.up) + playerPosition;
        Vector3 playerLeft = Vector3.Cross(playerPosition - myPosition, -Vector3.up) + playerPosition;
        
        SpawnSpore(playerPosition - myPosition);
        SpawnSpore(playerRight - myPosition);
        SpawnSpore(playerLeft - myPosition);
    }

    private void SpawnSpore(Vector3 direction)
    {
        Rigidbody spore = Instantiate(sporePrefab, transform.position, quaternion.identity).GetComponent<Rigidbody>();
        spore.AddForce(direction.normalized * sporeForce, ForceMode.Impulse);
    }
}
