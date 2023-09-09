using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Utilities;

namespace Units.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        #region Proprieties

        [SerializeField] private float _coolDownTimer;
        [SerializeField] private float _attackSpeed = 12;
        [SerializeField] private float _impulseForce = 5;
        public float AttackDamage = 8;

        private float _current; //Responsible for the movement using lerp
        [SerializeField] private AnimationCurve _evaluationCurve; //Used to create smoother movements

        [SerializeField] private float _maxX;
        [SerializeField] private float _minX;
        [SerializeField] private float _maxZ;
        [SerializeField] private float _minZ;
        
        private Vector3 _castPos, _goalPos; //Cast and goal position for movement
        private Vector2 _inputPos; //Touch position

        public bool IsSlashing { get; private set; } //Checks if player is slashing
        private bool _canSlash = true;

        //References
        private Player _player;

        #endregion

        private void Start()
        {
            _player = GetComponent<Player>();
        }

        private void Update() => Slash();
        
        /// <summary>
        /// Takes care of slash mechanic
        /// </summary>
        private void Slash()
        {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0)) && _canSlash)
            {
                _player.PlayerMovement.enabled = false;

                //Otherwise it moves the player in the correct direction and set attack delay
                Vector3 bossPosition = Helpers.GetBossPosition();
                Vector3 currentPosition = _player.PlayerTransform.position;
                _goalPos = new Vector3(bossPosition.x, currentPosition.y, bossPosition.z);
                Vector3 direction = _goalPos - currentPosition;

                StartCoroutine(SetAttackDelay());
                _goalPos = Helpers.CheckForOutScreen(_maxX, _minX, _maxZ, _minZ,  _goalPos);
                _player.PlayerTransform.DOMove(_goalPos + direction.normalized * _impulseForce, .5f).SetEase(Ease.OutSine);
                StartCoroutine(SetAttackCooldown(_coolDownTimer));
            }
        }

        private IEnumerator SetAttackCooldown(float timer)
        {
            _canSlash = false;
            yield return new WaitForSeconds(timer);
            _canSlash = true;
        }
    
        /// <summary>
        /// Wait for the end of the attack to set slashing to false
        /// </summary>
        /// <returns></returns>
        private IEnumerator SetAttackDelay()
        {
            IsSlashing = true;
            yield return new WaitForSeconds(0.5f);
            IsSlashing = false;
            _player.PlayerMovement.enabled = true;
        }
    }
}
