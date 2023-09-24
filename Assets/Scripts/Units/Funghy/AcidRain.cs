using System.Collections;
using UnityEngine;
using Utilities;

namespace Units.Funghy
{
    //Todo transform this in object pooling
    public class AcidRain : MonoBehaviour, IObserver
    {
        [SerializeField] private float _maxX, _minX, _maxZ, _minZ;
        [SerializeField] private GameObject _drop;
        [SerializeField] private float _spawnInterval;
        [SerializeField] private float _duration;
        
        private bool _isRaining;
        
        private Vector3 PickRandomPointInMap()
        {
            float randomX = Random.Range(_minX, _maxX);
            float randomZ = Random.Range(_minZ, _maxZ);
            Vector3 randomPos = new Vector3(randomX, 0, randomZ);
            return randomPos;
        }

        private IEnumerator SpawnRain()
        {
            while (_isRaining)
            {
                Instantiate(_drop, PickRandomPointInMap() + Vector3.up * 30, Quaternion.identity);
                yield return new WaitForSeconds(_spawnInterval);
            }
        }

        public IEnumerator Run()
        {
            yield return new WaitForSeconds(_duration);
            _isRaining = true;
            StartCoroutine(SpawnRain());
            yield return new WaitForSeconds(_duration);
            StartCoroutine(Rain());
        }

        private IEnumerator Rain()
        {
            yield return new WaitForSeconds(12f);
            _isRaining = false;
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
