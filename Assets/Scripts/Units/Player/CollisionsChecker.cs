using System;
using System.Collections;
using UnityEngine;

namespace Units.Player
{
    public class CollisionsChecker : MonoBehaviour
    {
        private Player _player;
        [SerializeField] private float _knockbackTreshold;
        
        public event EventHandler<OnCollisionArgs> OnCollision = delegate {  };

        private void Start() => _player = GetComponent<Player>();

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
                /*
                BaseUnit target = other.GetComponent<BaseUnit>();
                if (_player.PlayerAttack.IsSlashing)
                {
                    DealDamage(target);
                    return;
                }
                
                _player.PlayerHealth.RemoveLife(target.Damage);
                StartKnockBack(other);*/
            }
        }

        private void DealDamage(BaseUnit target)
        {
            VulnerableState vulnerableState = target.GetComponent<VulnerableState>();
            target.RemoveLife(vulnerableState != null && vulnerableState.IsVulnerable? _player.PlayerAttack.AttackDamage * 2 : _player.PlayerAttack.AttackDamage);
        }

        private void StartKnockBack(Collider col)
        {
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

    public class OnCollisionArgs : EventArgs
    {
        public BaseUnit Collider { get; set; }
    }
}
