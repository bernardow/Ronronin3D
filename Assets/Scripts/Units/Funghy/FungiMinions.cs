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

        private void Start() => _funghy = GetComponent<Funghy>();

        private IEnumerator SpawnMinions(float  timer)
        {
            for (int i = 0; i < _minionsNumber; i++)
            {
                Instantiate(_minionPrefab, transform);
                yield return new WaitForSeconds(timer);
            }
            _funghy.RunStateMachine();
        }

        public void OnNotify()
        {
            StartCoroutine(SpawnMinions(_spawnInterval));
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
