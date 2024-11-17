using System;
using System.Collections;
using System.Collections.Generic;
using Units;
using Units.Player;
using Unity.Mathematics;
using UnityEngine;
using Utilities;

public class ShootSpores : MonoBehaviour, IAttack, IObserver
{
    [SerializeField] private GameObject sporePrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float sporeForce = 3;
    
    public bool FinishedAttack { get; private set; }
    
    public void Attack()
    {
        SpawnSpores();
    }

    private void Update()
    {
#if UNITY_EDITOR
        Vector3 myPosition = transform.position;
        Vector3 playerPosition = Player.Instance.PlayerTransform.position;
        Vector3 playerRight = Vector3.Cross(playerPosition - myPosition, Vector3.up) + playerPosition;
        Vector3 playerLeft = Vector3.Cross(playerPosition - myPosition, -Vector3.up) + playerPosition;
        
        Debug.DrawLine(playerPosition, myPosition, Color.blue);
        Debug.DrawLine(playerLeft, myPosition, Color.red);
        Debug.DrawLine(playerRight, myPosition, Color.green);
#endif
        if (gameObject.CompareTag("Boss")) return;
        transform.LookAt(Player.Instance.PlayerTransform);
    }
    
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

    private void SpawnSpore(Vector3 direction, Vector3 origin = new Vector3())
    {
        Vector3 spawnPoint = origin == new Vector3() ? this.spawnPoint.position : origin;
        Rigidbody spore = Instantiate(sporePrefab, spawnPoint, quaternion.identity).GetComponent<Rigidbody>();
        spore.AddForce(direction.normalized * sporeForce, ForceMode.Impulse);
    }
    

    public void OnNotify()
    {
        StartCoroutine(Run());
    }

    public IEnumerator Run()
    {
        FungiUltimate fungiUltimate = GetComponent<FungiUltimate>();
        Vector3 spawnPoint = fungiUltimate._fungiCenter.position;
        
        Vector3 south = -fungiUltimate._fungiCenter.localPosition + Vector3.forward;
        Vector3 north = -south;
        Vector3 east = fungiUltimate._fungiCenter.localPosition + Vector3.right;
        Vector3 west = -east;
        Vector3 southwest = south + west;
        Vector3 northwest = north + west;
        Vector3 southeast = south + east;
        Vector3 northeast = north + east;
        
        for (int i = 0; i < 10; i++)
        {
            SpawnSpore(south);
            SpawnSpore(north);
            SpawnSpore(east);
            SpawnSpore(west);
            SpawnSpore(southwest);
            SpawnSpore(northwest);
            SpawnSpore(southeast);
            SpawnSpore(northeast);
            fungiUltimate.RotateCenter(25);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void Disable()
    {
        StopAllCoroutines();
        enabled = false;
    }

    public void Enable()
    {
        enabled = true;
    }
}
