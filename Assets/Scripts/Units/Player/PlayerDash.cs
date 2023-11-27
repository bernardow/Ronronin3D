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
        [SerializeField] private LayerMask _mask;
        public bool IsDashing { get; private set; }
        public bool HaveDash { get; set; }
        public float DashImpulse;
        public float DashCooldown = 1;
        
        private void Awake() => _player = GetComponent<Player>();
        
        void Start() => _player.PlayerInputs.OnDashActivate += Dash;

        private void OnDestroy() => _player.PlayerInputs.OnDashActivate -= Dash;

        //DEBUG
        private void Update() => Debug.DrawRay(_player.PlayerTransform.localPosition, _player.PlayerInputs.MovementDirectionRaw * DashImpulse, Color.magenta);
        

        private void Dash()
        {
            if (_canDash && HaveDash)
            {
                Vector3 goalPos = _player.PlayerInputs.MovementDirectionRaw * DashImpulse; //+ _player.PlayerTransform.localPosition;
                goalPos = Helpers.CheckForInSetupCollision(goalPos, DashImpulse, _player.PlayerTransform, _player.PlayerCollider, _mask);
                goalPos += _player.PlayerTransform.localPosition;
                goalPos = Helpers.CheckForOutScreen(goalPos, 22, -20, 17.7f, -8.7f);
                _player.PlayerTransform.DOMove(goalPos, 0.25f).SetEase(Ease.Linear);
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
