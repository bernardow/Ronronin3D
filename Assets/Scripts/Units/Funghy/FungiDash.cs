using System;
using System.Collections;
using System.Threading.Tasks;
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
            _direction = new Vector3(Random.Range(0, 100), _funghy.FungiTransform.position.y, Random.Range(0, 100));
        }
        
        private void FixedUpdate()
        {
            if (_isDashing)
            {
                _counter += Time.deltaTime;
                _funghy.FungyRigidbody.AddForce(_direction * _dashForce, ForceMode.Force);
            }

            if (_counter >= _duration)
            {
                _isDashing = false;
                _counter = 0;
                //_funghy.ManageIdleMovement();
                _funghy.RunStateMachine();
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.collider.CompareTag("Ground"))
                _direction = Vector3.Cross(Vector3.up, _direction);
        }

        public void OnNotify()
        {
            //_funghy.ManageIdleMovement();
            _isDashing = true;
        }
    }
}
