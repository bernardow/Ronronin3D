using System;
using Photon.Pun;
using UnityEngine;

namespace Units.Funghy
{
    public class SporeUnit : MonoBehaviour
    {
        private BaseUnit _baseUnit;

        private void Start() => _baseUnit = GetComponent<BaseUnit>();

        private void OnDisable()
        {
            _baseUnit.Life = 4;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("Player"))
                _baseUnit.CallSelfDestroy();

            _baseUnit.RemoveLife(1);
        }
    }
}
