using System;
using UnityEditor;
using UnityEngine;

namespace Units.Player
{
    public class KunaiUnit : MonoBehaviour
    {
        [SerializeField] private float _damage = 4;
        [SerializeField] private float _life = 3;

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("Player")) return;
            
            if (other.collider.CompareTag("Boss") || other.collider.CompareTag("Projectiles"))
            {
                BaseUnit baseUnit = other.collider.GetComponent<BaseUnit>();
                baseUnit.RemoveLife(_damage);
                Destroy(gameObject);
            }
            
            _life--;
            CheckLife();
        }

        private void CheckLife()
        {
            if (_life <= 0)
                Destroy(gameObject);
        }
    }
}
