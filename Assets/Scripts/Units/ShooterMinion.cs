using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Units;
using Units.Player;
using UnityEngine;

public class ShooterMinion : BaseUnit, IAttack
{
    public GameObject Bomb;
    public float Force = 10;
    
    private Transform Transform => transform;

    private IEnumerator ShooterLogic()
    {
        Vector3 myPosition = Transform.position;
        
        for (int i = 0; i < 3; i++)
        {
            Transform bombTransform = Instantiate(Bomb, myPosition, Quaternion.identity).GetComponent<Transform>();
            StartCoroutine(ShootBomb(bombTransform));
            //bombRigidbody.AddForce((Player.Instance.PlayerTransform.position - myPosition).normalized * Force + (Vector3.up * Force), ForceMode.Impulse);
                
            yield return new WaitForSeconds(0.5f);
        }
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
        StartCoroutine(ShooterLogic());
    }
}
