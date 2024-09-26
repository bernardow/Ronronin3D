using System.Collections;
using System.Collections.Generic;
using Units;
using Units.Player;
using UnityEngine;

public class ShootSpores : BaseUnit, IAttack
{
    [SerializeField] private GameObject sporePrefab;
    [SerializeField] private float sporeForce = 3;
    
    public void Attack()
    {
        StartCoroutine(SpawnSpores());
    }

    private IEnumerator SpawnSpores()
    {
        Vector3 playerPosition = Player.Instance.PlayerTransform.position;

        for (int i = 0; i < 3; i++)
        {
            Rigidbody spore = Instantiate(sporePrefab, transform).GetComponent<Rigidbody>();
            spore.AddForce((playerPosition - transform.position).normalized * sporeForce, ForceMode.Impulse);
            
            yield return new WaitForSeconds(0.5f);
        }
    }
}
