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
        private float _counter;
        private Vector3 _direction;
        private bool _isDashing;

        private void Awake()
        {
            _funghy = GetComponent<Funghy>();
        }

        private void Start()
        {
            _direction = new Vector3(Random.Range(0, 100), _funghy.FungiTransform.localPosition.y, Random.Range(0, 100));
        }
        
        private void FixedUpdate()
        {
            if (_isDashing)
            {
                _counter += Time.deltaTime;
                _funghy.FungiTransform.position += _direction * _dashForce * Time.deltaTime;
                Debug.DrawLine(transform.localPosition, _direction, Color.red);
            }

            if (_counter >= _duration)
            {
                _isDashing = false;
                _counter = 0;
                _funghy.RunStateMachine();
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.collider.CompareTag("Ground"))
            {
                _direction = FungiUtilities.ChangeDirection(_direction, FungiUtilities.ChangeTypes.CROSS);
                _direction.Normalize();
                Ray ray = new Ray(_funghy.FungiTransform.localPosition, _direction * 0.1f);
                if (Physics.Raycast(ray, out RaycastHit hit, 1))
                {
                    if (hit.collider.CompareTag("Setup"))
                        _direction = -_direction;
                }
            }
        }

        public void OnNotify()
        {
            _funghy.ManageIdleMovement();
            _isDashing = true;
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
