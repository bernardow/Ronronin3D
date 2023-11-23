using System.Collections;
using UnityEngine;

namespace Units.Player
{
    public class PlayerBasicAttack : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _swordPrefab;
        [SerializeField] private float _coolDownDuration = 0.25f;
        public bool CanAttack { get; private set; } = true;
    
        private Player _player;

        private void Start()
        {
            _player = GetComponent<Player>();
            _player.PlayerInputs.OnBasicAttack += BasicAttack;
        }
        
        private void BasicAttack()
        {
            if (!CanAttack) return;
            StartCoroutine(BasicAttackCooldown(_coolDownDuration));
        }

        private IEnumerator BasicAttackCooldown(float timer)
        {
            yield return new WaitForSeconds(0.5f);
            CanAttack = false;
            yield return new WaitForSeconds(timer);
            CanAttack = true;
        }
    }
}
