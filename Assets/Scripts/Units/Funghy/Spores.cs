using System.Collections;
using UnityEngine;
using Utilities;

namespace Units.Funghy
{
    public class Spores : MonoBehaviour, IObserver
    {
        [SerializeField] private Transform _fungiCenter;
        private Funghy _funghy;
        [SerializeField] private GameObject _sporePrefab;
        [SerializeField] private float _shotForce;

        private bool _isShooting;
        
        // Start is called before the first frame update
        void Start() => _funghy = GetComponent<Funghy>();

        private void Update()
        {
            if (_isShooting)
                RotateCenter();
        }

        private void SpawnSpores(Vector3 direction)
        {
            GameObject newSpore = Instantiate(_sporePrefab, _fungiCenter);
            Rigidbody2D newSporeRb = newSpore.GetComponent<Rigidbody2D>();
            
            newSporeRb.AddForce(direction * _shotForce, ForceMode2D.Impulse);
        }

        private void RotateCenter() => _fungiCenter.Rotate(-Vector3.forward * 10 * Time.deltaTime);

        private IEnumerator Shooter()
        {
            for (int i = 0; i < 4; i++)
            {
                SpawnSpores(_fungiCenter.up);
                SpawnSpores(-_fungiCenter.up);
                SpawnSpores(_fungiCenter.right);
                SpawnSpores(-_fungiCenter.right);
                yield return new WaitForSeconds(3f);
            }
        }
        
        private IEnumerator SetAttackTime(float timer)
        {
            _isShooting = true;
            StartCoroutine(Shooter());
            yield return new WaitForSeconds(timer);
            _isShooting = false;
            _funghy.RunStateMachine();
        }
        
        public void OnNotify()
        {
            StartCoroutine(SetAttackTime(12));
        }
    }
}
