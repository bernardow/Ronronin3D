using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace Units.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        #region Proprieties
        
        public float PlayerSpeed = 100;
        [SerializeField] private bool _rigidbody;
        private bool _canMove = true;
        private Player _player;
        public CharacterController CharacterController;
        
        #endregion

        private void Start()
        {
            _player = GetComponent<Player>();
            _player.PlayerInputs.OnDashActivate += DisableMovement;
            CharacterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Vector3 bossPosition = Helpers.GetBossPosition();
            Vector3 lookPosition =
                new Vector3(bossPosition.x, 0, bossPosition.z) - _player.PlayerTransform.localPosition;
            //_player.PlayerTransform.localRotation = Quaternion.LookRotation(lookPosition);
             CharacterController characterController = new CharacterController();
             
             Move();
        }
         
        // /void FixedUpdate() => Move();
        
        private void Move()
        {
            if (!_rigidbody && _canMove)
            {
                Vector3 cameraRight = Camera.main.transform.right * _player.PlayerInputs.Horizontal;
                Vector3 cameraUp = Camera.main.transform.up * _player.PlayerInputs.Vertical;
                Vector3 directionToMove = (cameraRight + cameraUp) * PlayerSpeed;
                directionToMove.y = 0.5f;
                CharacterController.SimpleMove(directionToMove);
                //_player.PlayerTransform.position += _player.PlayerInputs.MovementDirection * PlayerSpeed * Time.deltaTime;
                return;
            }
            
            if(_canMove)
                _player.PlayerRigidbody.AddForce(_player.PlayerInputs.MovementDirection * PlayerSpeed, ForceMode.Force);
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.collider.CompareTag("DeadZone"))
                SceneManager.LoadScene(1);
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
