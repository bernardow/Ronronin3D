using System;
using System.Collections;
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
        
        private void Start()
        {
            _funghy = GetComponent<Funghy>();
            _direction = new Vector3(Random.Range(0, 100), 0, Random.Range(0, 100));
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
            _isDashing = true;
        }
    }
}
