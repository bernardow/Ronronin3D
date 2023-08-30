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
            if (other.gameObject.layer == 3)
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
                StartCoroutine(StartKnockBack(other.collider));
            }
        }

        private IEnumerator StartKnockBack(Collider col)
        {
            Vector3 enemyPosition = col.transform.position;
            Vector3 playerPosition = _player.PlayerTransform.position;
            yield return new WaitForSeconds(0.2f);
            KnockBack(playerPosition, enemyPosition);
        }

        private void KnockBack(Vector3 playerPosition,  Vector3 enemyPosition)
        {
            Vector3 direction = playerPosition - enemyPosition;
            direction = enemyPosition == playerPosition ? Vector3.up : direction;
            float knockbackForce = _knockbackTreshold / Vector3.Distance(enemyPosition, playerPosition); 
            _player.PlayerRigidbody.AddForce(direction * knockbackForce, ForceMode.Impulse);
        }

        /*
        private IEnumerator StartBlink(Collider col = null)
        {
            Blink(out Material blinkMaterial, col);
            yield return new WaitForSeconds(.5f);
            blinkMaterial.SetFloat("_Blink", 0);
        }
        
        
        private void Blink(out Material blinkMaterial, Collider col = null)
        {
            col = col == null? GetComponent<Collider>() : col!.GetComponent<Collider>();
            SpriteRenderer spriteRenderer = col.GetComponent<SpriteRenderer>();
            Material blinkMat = spriteRenderer.sharedMaterial;
            
            
            blinkMat.SetFloat("_Blink", 1);
            blinkMaterial = blinkMat;
        }*/
    }
}
