using System.Collections;
using UnityEngine;
using Utilities;

namespace Units.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        #region Proprieties
        
        [SerializeField] private float _speed = 1;
        [SerializeField] private bool _rigidbody;
        private bool _canMove = true;
        private Player _player;
        
        #endregion

        private void Start()
        {
            _player = GetComponent<Player>();
            _player.PlayerInputs.OnDashActivate += DisableMovement;
        }

        void FixedUpdate() => Move();
        
        private void Move()
        {
            if (!_rigidbody && _canMove)
            {
                _player.PlayerTransform.position += _player.PlayerInputs.MovementDirection * _speed * Time.deltaTime;
                return;
            }
            
            if(_canMove)
                _player.PlayerRigidbody.AddForce(_player.PlayerInputs.MovementDirection * _speed, ForceMode.Force);
        }

        private void DisableMovement()
        {
            StartCoroutine(DisableMovementCoroutine());
        }

        private IEnumerator DisableMovementCoroutine()
        {
            _canMove = false;
            yield return new WaitForSeconds(0.25f);
            _canMove = true;
        }
    }
}
