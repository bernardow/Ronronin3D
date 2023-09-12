using System;
using System.Collections;
using UnityEngine;

namespace Units.Player
{
    public class CollisionsChecker : MonoBehaviour
    {
        private Player _player;
        [SerializeField] private float _knockbackTreshold;

        private void Start() => _player = GetComponent<Player>();

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer == 3 || other.gameObject.layer == 7)
            {
                BaseUnit target = other.collider.GetComponent<BaseUnit>();
                if (_player.PlayerAttack.IsSlashing)
                {
                    target.RemoveLife(_player.PlayerAttack.AttackDamage);
                    //StartCoroutine(StartBlink(other.collider));
                    return;
                }
                
                _player.PlayerHealth.RemoveLife(target.Damage);
                //StartCoroutine(StartBlink());
                StartKnockBack(other.collider);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 3 || other.gameObject.layer == 7)
            {
                BaseUnit target = other.GetComponent<BaseUnit>();
                if (_player.PlayerAttack.IsSlashing)
                {
                    target.RemoveLife(_player.PlayerAttack.AttackDamage);
                    //StartCoroutine(StartBlink(other.collider));
                    return;
                }
                
                _player.PlayerHealth.RemoveLife(target.Damage);
                //StartCoroutine(StartBlink());
                StartKnockBack(other);
            }
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
}
