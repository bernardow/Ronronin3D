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
        private Player _player;
        
        #endregion

        private void Start() =>_player = GetComponent<Player>();

        void FixedUpdate() => Move();
        
        private void Move()
        {
            if (Input.anyKey)
            {
                if (!_rigidbody)
                {
                    _player.PlayerTransform.position += Helpers.GetDirection() * _speed * Time.deltaTime;
                    return;
                }

                _player.PlayerRigidbody.AddForce(Helpers.GetDirection() * _speed, ForceMode.Force);
            }
        }
    }
}
