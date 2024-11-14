using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Units.Funghy
{
    public class FungiDash : MonoBehaviour, IObserver
    {
        private Funghy _funghy;

        [SerializeField] private float _duration;
        [SerializeField] private float _dashForce;
        [SerializeField] private LayerMask _mask;
        private float _counter;
        private Vector3 _direction;
        private bool _isDashing;
        
        private void Awake()
        {
            _funghy = GetComponent<Funghy>();
        }

        private void Start()
        {
            _direction = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
        }
        
        private void Update()
        {
            if (_isDashing)
            {
                _counter += Time.deltaTime;
                _funghy.FungiTransform.position += _direction * _dashForce * Time.deltaTime;
                
                /*  DEBUG
                Vector3 leftPoint = Vector3.Cross(_direction, Vector3.up) * 2f;
                Vector3 rightPoint = -leftPoint;
                
                Debug.DrawRay(leftPoint + transform.localPosition, _direction * _rayCastRange, Color.red);
                Debug.DrawRay(rightPoint + transform.localPosition, _direction * _rayCastRange, Color.red);
                */
            }

            if (_counter >= _duration)
            {
                _isDashing = false;
                _counter = 0;
                _funghy.FungyRigidbody.AddForce(_direction * _dashForce * 0.5f, ForceMode.Impulse);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.collider.CompareTag("Ground") && other.collider.gameObject.layer != 8)
            {
                _direction = FungiUtilities.ChangeDirection(_direction, FungiUtilities.ChangeTypes.CROSS);
                _direction.Normalize();
                _direction = Helpers.SearchForWalls(_funghy.FungiTransform.localPosition, _direction, _mask);
                StartCoroutine(SecondSearch());
            }
        }
        
        private IEnumerator SecondSearch()
        {
            yield return new WaitForSeconds(1f);
            _direction = Helpers.SearchForWalls(_funghy.FungiTransform.localPosition, _direction, _mask);
            
        }

        public IEnumerator Run()
        {
            if (_funghy == null)
                _funghy = GetComponent<Funghy>();
            _funghy.ManageIdleMovement(false);
            _isDashing = false;
            yield return new WaitForSeconds(2);
            _funghy.FungyRigidbody.AddForce(_direction * _dashForce * 0.5f, ForceMode.Impulse);
            yield return new WaitForSeconds(0.5f);
            _isDashing = true;
            yield return new WaitForSeconds(_duration);
        }

        public void OnNotify()
        {
            StartCoroutine(Run());
        }

        public void Disable()
        {
            enabled = false;
            StopAllCoroutines();
        }

        public void Enable()
        {
            enabled = true;
        }
    }
}
