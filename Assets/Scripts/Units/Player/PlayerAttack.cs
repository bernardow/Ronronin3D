using System.Collections;
using TMPro;
using UnityEngine;
using Utilities;

namespace Units.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        #region Proprieties
        
        [SerializeField] private float _attackSpeed = 12;
        [SerializeField] private float _impulseForce = 5;
        public float AttackDamage = 8;

        private float _counter; //Used to see if the user held the key or finger for > 0.8f
        private float _current; //Responsible for the movement using lerp
        [SerializeField] private AnimationCurve _evaluationCurve; //Used to create smoother movements

        [SerializeField] private float _maxX;
        [SerializeField] private float _minX;
        [SerializeField] private float _maxZ;
        [SerializeField] private float _minZ;
        
        private Vector3 _castPos, _goalPos; //Cast and goal position for movement
        private Vector2 _inputPos; //Touch position

        public bool IsSlashing { get; private set; } //Checks if player is slashing

        //References
        private Player _player;
        private Material _material;

        #endregion

        private void Start()
        {
            _player = GetComponent<Player>();
            _material = GetComponent<MeshRenderer>().sharedMaterial;
        }

        private void Update() => Slash();
        
        /// <summary>
        /// Takes care of slash mechanic
        /// </summary>
        private void Slash()
        {
            //Touch setup
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Joystick1Button0))
            {
                //Disables player movement and increases counter
                _counter += Time.deltaTime;
                _player.PlayerMovement.enabled = false;

                if (_counter >= 0.8f)
                {
                    _material.SetInt("_Blink", 1);
                }
            }
            else if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Joystick1Button0))
            {
                _material.SetInt("_Blink", 0);
                
                //if counter it's lesser than 0.8f nothing happens and player movement get active again
                if (_counter <= 0.8f)
                {
                    _player.PlayerMovement.enabled = true;
                    return;
                }

                //Otherwise it moves the player in the correct direction and set attack delay
                Vector3 currentDir = Helpers.GetDirection();

                StartCoroutine(SetAttackDelay());
                _current = 0;
                _castPos = transform.position;
                _goalPos = _castPos + currentDir * _impulseForce;
                _goalPos = Helpers.CheckForOutScreen(_maxX, _minX, _maxZ, _minZ,  _goalPos);
                _counter = 0;
            }

            //handles the movement of the slash trough lerp
            if (IsSlashing)
            {
                _current = Mathf.MoveTowards(_current, 1, _attackSpeed * Time.deltaTime);
                _player.PlayerTransform.position = Vector3.Lerp(_castPos, _goalPos, _evaluationCurve.Evaluate(_current));
            }
        }
    
        /// <summary>
        /// Wait for the end of the attack to set slashing to false
        /// </summary>
        /// <returns></returns>
        private IEnumerator SetAttackDelay()
        {
            IsSlashing = true;
            yield return new WaitForSeconds(0.75f);
            IsSlashing = false;
            _player.PlayerMovement.enabled = true;
        }
    }
}
