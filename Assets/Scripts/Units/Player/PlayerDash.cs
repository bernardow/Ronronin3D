using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Utilities;

namespace Units.Player
{
    public class PlayerDash : MonoBehaviour
    {
        private Player _player;
        private bool _canDash = true;
        [SerializeField] private float m_StaminaCost = 5;
        [SerializeField] private LayerMask _mask;
        public bool IsDashing { get; private set; }
        public bool HaveDash { get; set; }
        public float DashImpulse;
        public float DashCooldown = 1;

        public bool Unlocked;
        
        private void Awake() => _player = GetComponent<Player>();
        
        void Start() => _player.PlayerInputs.OnDashActivate += Dash;

        private void OnDestroy() => _player.PlayerInputs.OnDashActivate -= Dash;

        //DEBUG
        private void Update() => Debug.DrawRay(_player.PlayerTransform.localPosition, _player.PlayerInputs.MovementDirectionRaw * DashImpulse, Color.magenta);
        

        private void Dash()
        {
            if (!Unlocked) return;
            
            if (_canDash && HaveDash)
            {
                Vector3 cameraRight = Camera.main.transform.right * _player.PlayerInputs.Horizontal;
                Vector3 cameraUp = Camera.main.transform.up * _player.PlayerInputs.Vertical;
                Vector3 directionToMove = (cameraRight + cameraUp).normalized * DashImpulse;
                directionToMove.y = 0;
                directionToMove = directionToMove == Vector3.zero ? Player.Instance.transform.forward : directionToMove;

                directionToMove = Helpers.CheckForInSetupCollision(directionToMove, DashImpulse, _player.PlayerTransform, _player.PlayerCollider, _mask);
                directionToMove += _player.PlayerTransform.localPosition;
                //directionToMove = Helpers.CheckForOutScreen(directionToMove, 22, -20, 17.7f, -8.7f);
                _player.PlayerTransform.DOMove(directionToMove, 0.25f).SetEase(Ease.Linear);
                StartCoroutine(IsDashingPropHandler());
                StartCoroutine(SetDashCooldown());
            }
        }


        private IEnumerator IsDashingPropHandler()
        {
            IsDashing = true;
            yield return new WaitForSeconds(0.25f);
            IsDashing = false;
        }

        private IEnumerator SetDashCooldown()
        {
            _canDash = false;
            yield return new WaitForSeconds(DashCooldown);
            _canDash = true;
        }
    }
}
