using System.Collections;
using UnityEngine;
using Utilities;

namespace Units.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        #region Proprieties
        
        [SerializeField] private float _speed = 1;
        [SerializeField] private AnimationCurve _evaluationCurve;

        [SerializeField] private bool mobile;

        private Player _player;
        private float _current;
        private Vector3 _castPos, _goalPos;
        private Vector2 _inputPos;

        private bool _canMove = true;
        private bool _isMoving;
        
        #endregion

        private void Start()
        {
            _player = GetComponent<Player>();
            StartCoroutine(SetMoveDelay());
        }

        void Update()
        {
            if (!mobile)
            {
                Move();
                if (_isMoving)
                {
                    _current = Mathf.MoveTowards(_current, 1, _speed * Time.deltaTime);
                    _player.PlayerTransform.position = Vector3.Lerp(_castPos, _goalPos, _evaluationCurve.Evaluate(_current));
                }
            }
        }
    
        private void Move()
        {
            if (Input.anyKeyDown)
            {
                _current = 0;
                _castPos = _player.PlayerTransform.position;
                _goalPos = _castPos + Helpers.GetDirection() * 0.5f;
                StartCoroutine(SetMoveDelay());
            }
        }

        /// <summary>
        /// Basically the cooldown for the movement
        /// </summary>
        /// <returns></returns>
        private IEnumerator SetMoveDelay()
        {
            _canMove = false;
            _isMoving = true;
            yield return new WaitForSeconds(0.2f);
            _canMove = true;
            _isMoving = false;
        }
    }
}
