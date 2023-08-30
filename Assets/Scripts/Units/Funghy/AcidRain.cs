using System.Collections;
using UnityEngine;
using Utilities;

namespace Units.Funghy
{
    //Todo transform this in object pooling
    public class AcidRain : MonoBehaviour, IObserver
    {
        [SerializeField] private float _maxX, _minX, _maxZ, _minZ;
        [SerializeField] private GameObject _shadow;
        [SerializeField] private float _spawnInterval;
        
        private Funghy _funghy;

        private bool _isRaining;
        
        private void Start() => _funghy = GetComponent<Funghy>();

        private Vector3 PickRandomPointInMap()
        {
            float randomX = Random.Range(_minX, _maxX);
            float randomZ = Random.Range(_minZ, _maxZ);
            Vector3 randomPos = new Vector3(randomX, -0.8f, randomZ);
            return randomPos;
        }

        private IEnumerator SpawnRain()
        {
            while (_isRaining)
            {
                Instantiate(_shadow, PickRandomPointInMap(), Quaternion.identity);
                yield return new WaitForSeconds(_spawnInterval);
            }
        }

        private IEnumerator RunRain(float timer)
        {
            yield return new WaitForSeconds(timer);
            _isRaining = true;
            StartCoroutine(SpawnRain());
            yield return new WaitForSeconds(12f);
            _isRaining = false;
            _funghy.RunStateMachine();
        }
        
        public void OnNotify()
        {
            StartCoroutine(RunRain(2f));
        }
    }
}
