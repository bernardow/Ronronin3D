using System;
using System.Collections;
using UnityEngine;

namespace Units.Player
{
    public class CollisionsChecker : MonoBehaviour
    {
        private Player _player;
        [SerializeField] private float _knockbackTreshold;
        [SerializeField] private LayerMask _mask;
        
        public event EventHandler<OnCollisionArgs> OnCollision = delegate {  };

        private void Start()
        {
            _player = GetComponent<Player>();
            OnCollision += StartKnockBack;
            _player.PlayerInputs.OnDashActivate += DisableKnockBack;
        }

        private void DisableKnockBack()
        {
            StartCoroutine(DisableKnockBackCoroutine());
        }

        private IEnumerator DisableKnockBackCoroutine()
        {
            OnCollision -= StartKnockBack;
            _player.PlayerCollider.excludeLayers = _mask;
            yield return new WaitForSeconds(0.25f);
            _player.PlayerCollider.excludeLayers = 0;
            OnCollision += StartKnockBack;
        }

        private void OnCollisionEnter(Collision other) => CheckColliders(other.collider);

        private void OnTriggerEnter(Collider other) => CheckColliders(other);
        

        private void CheckColliders(Collider other)
        {
            if (other.gameObject.layer == 3 || other.gameObject.layer == 7)
            {
                OnCollisionArgs args = new OnCollisionArgs
                {
                    Collider = other.GetComponent<BaseUnit>()
                };
                OnCollision.Invoke(null, args);
            }
        }

        private void StartKnockBack(object sender, OnCollisionArgs args)
        {
            Collider col = args.Collider.GetComponent<Collider>();
            Vector3 enemyPosition = col.transform.position;
            Vector3 playerPosition = _player.PlayerTransform.position;
            KnockBack(playerPosition, enemyPosition);
        }

        private void KnockBack(Vector3 playerPosition,  Vector3 enemyPosition)
        {
            Vector3 direction = playerPosition - enemyPosition;
            direction = enemyPosition == playerPosition ? Vector3.up : direction;
            float knockbackForce = _knockbackTreshold / Vector3.Distance(enemyPosition, playerPosition); 
            _player.PlayerRigidbody.AddForce(direction * knockbackForce, ForceMode.Impulse);
        }
    }
}
