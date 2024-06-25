using System;
using UnityEngine;

namespace Units.Funghy
{
    public class SporeUnit : MonoBehaviour
    {
        private BaseUnit _baseUnit;
        public float Lifetime = 10f;
        
        private void Start() => _baseUnit = GetComponent<BaseUnit>();

        private void Update()
        {
            Lifetime -= Time.deltaTime;

            if (Lifetime <= 0)
                gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            _baseUnit.Life = 4;
            Lifetime = 10;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("Player"))
                _baseUnit.SelfDestroy();

            _baseUnit.RemoveLife(1);
        }
    }
}
