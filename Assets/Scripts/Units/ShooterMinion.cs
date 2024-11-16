using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Units;
using Units.Player;
using UnityEngine;
using Utilities;

public class ShooterMinion : MonoBehaviour, IAttack, IObserver
{
    public GameObject Bomb;
    
    private Transform Transform;

    private void Awake()
    {
        Transform = transform;
    }

    public bool FinishedAttack { get; private set; }
    
    private IEnumerator ShooterLogic(Vector3 spawnPosition = new Vector3())
    {
        Vector3 myPosition = spawnPosition == new Vector3()? Transform.position : spawnPosition;
        
        for (int i = 0; i < 3; i++)
        {
            Transform bombTransform = Instantiate(Bomb, myPosition, Quaternion.identity).GetComponent<Transform>();
            StartCoroutine(ShootBomb(bombTransform));
            //bombRigidbody.AddForce((Player.Instance.PlayerTransform.position - myPosition).normalized * Force + (Vector3.up * Force), ForceMode.Impulse);
                
            yield return new WaitForSeconds(0.5f);
        }

        FinishedAttack = true;
    }

    private IEnumerator ShootBomb(Transform bombTransform)
    {
        Vector3 initialPosition = bombTransform.position;
        Vector3 goalPosition = Player.Instance.PlayerTransform.position;
        
        float current = 0;
        while (current < 1)
        {
            current = Mathf.MoveTowards(current, 1, 1 * Time.deltaTime);
            bombTransform.position = Vector3.Lerp(initialPosition, goalPosition, current);
            yield return null;
        }
    }

    public void Attack()
    {
        FinishedAttack = false;
        StartCoroutine(ShooterLogic());
    }

    public void OnNotify()
    {
        Transform = GetComponentInChildren<MeshRenderer>().transform;
        StartCoroutine(Run());
    }

    public IEnumerator Run()
    {
        Transform = GetComponentInChildren<MeshRenderer>().transform;
        yield return ShooterLogic(transform.position);
        yield return ShooterLogic(transform.position);
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
