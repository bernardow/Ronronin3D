using System;
using UnityEditor;
using UnityEngine;

namespace Units.Player
{
    public class KunaiUnit : MonoBehaviour
    {
        [SerializeField] private int _damage = 4;
        [SerializeField] private float _life = 3;

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("Player")) return;
            
            if (other.collider.CompareTag("Boss") || other.collider.CompareTag("Projectiles"))
            {
                BaseUnit baseUnit = other.collider.GetComponent<BaseUnit>();
                //baseUnit.RemoveBossLife(_damage);
                AutoDestroy();
            }
            
            _life--;
            CheckLife();
        }

        private void CheckLife()
        {
            if (_life <= 0)
                AutoDestroy();
        }
        public void AutoDestroy() => Destroy(gameObject);
    }
}
