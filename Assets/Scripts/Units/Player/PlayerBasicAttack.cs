using System.Collections;
using UnityEngine;

namespace Units.Player
{
    public class PlayerBasicAttack : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _swordPrefab;
        [SerializeField] private float _coolDownDuration = 0.25f;
        private bool _canAttack = true;
        private Player _player;

        private void Start()
        {
            _player = GetComponent<Player>();
            _player.PlayerInputs.OnBasicAttack += BasicAttack;
        }
        
        private void BasicAttack()
        {
            if (!_canAttack)
                return;
            StartCoroutine(BasicAttackCooldown(_coolDownDuration));
        }

        private IEnumerator BasicAttackCooldown(float timer)
        {
            _canAttack = false;
            yield return new WaitForSeconds(timer);
            _canAttack = true;
        }
    }
}
