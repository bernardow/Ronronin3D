using System;
using UnityEngine;

namespace Units.Player
{
    public class KunaiUnit : MonoBehaviour
    {
        [SerializeField] private float _damage = 4;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Boss") || other.CompareTag("Projectiles"))
            {
                BaseUnit baseUnit = other.GetComponent<BaseUnit>();
                baseUnit.RemoveLife(_damage);
            }

            if(!other.CompareTag("Player"))
                Destroy(gameObject);
        }
    }
}
