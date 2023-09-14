using System.Collections;
using UnityEngine;
using Utilities;

namespace Units.Funghy
{
    public class Spores : MonoBehaviour, IObserver
    {
        [SerializeField] private Transform _fungiCenter;
        private Funghy _funghy;
        [SerializeField] private GameObject[] _sporePool;
        [SerializeField] private float _shotForce;

        private bool _isShooting;
        
        // Start is called before the first frame update
        void Start() => _funghy = GetComponent<Funghy>();

        private void Update()
        {
            if (_isShooting)
                RotateCenter();
        }

        private void GetSpores(Vector3 direction)
        {
            foreach (GameObject spore in _sporePool)
            {
                if (!spore.activeSelf)
                {
                    spore.SetActive(true);
                    spore.transform.position = _fungiCenter.position;
                    Rigidbody newSporeRb = spore.GetComponent<Rigidbody>();
                    newSporeRb.AddForce(direction * _shotForce, ForceMode.Impulse);
                    break;
                }
            }
        }

        private void RotateCenter() => _fungiCenter.Rotate(-Vector3.up * 10 * Time.deltaTime);

        private IEnumerator Shooter()
        {
            for (int i = 0; i < 4; i++)
            {
                GetSpores(_fungiCenter.forward);
                GetSpores(-_fungiCenter.forward);
                GetSpores(_fungiCenter.right);
                GetSpores(-_fungiCenter.right);
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
