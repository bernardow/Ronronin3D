using System;
using System.Collections;
using UnityEngine;
using Utilities;

namespace Units.Funghy
{
    public class FungiMinions : MonoBehaviour, IObserver
    {
        [SerializeField] private GameObject _minionPrefab;
        [SerializeField] private float _spawnInterval = 0.5f;
        [SerializeField] private int _minionsNumber = 4;
        private bool _isSpawning;

        private Funghy _funghy;

        private void Awake() => _funghy = GetComponent<Funghy>();

        public IEnumerator Run()
        {
            _funghy.FungiCenter.LookAt(Helpers.GetPlayerPosition());
            
            for (int i = 0; i < _minionsNumber; i++)
            {
                Transform minion = Instantiate(_minionPrefab, _funghy.FungiCenter).transform;
                Vector3 position = Vector3.zero;
                position.x = (i - 2) * 0.5f;
                position.z = -1 * (position.x * position.x * 0.1f - 2);
                minion.transform.localPosition += position;
                minion.transform.SetParent(transform.parent.parent);
                yield return new WaitForSeconds(_spawnInterval);
            }

            yield return null;
        }

        public void OnNotify()
        {
            StartCoroutine(Run());
        }

        public void Disable()
        {
            enabled = false;
        }

        public void Enable()
        {
            enabled = true;
        }
    }
}
