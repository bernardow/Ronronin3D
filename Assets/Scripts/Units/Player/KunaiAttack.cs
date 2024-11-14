using System;
using System.Collections;
using UnityEngine;
using Utilities;

namespace Units.Player
{
    public class KunaiAttack : MonoBehaviour
    {
        [SerializeField] private GameObject _kunai;
        [SerializeField] private Transform _kunaiSpawnPosition;
        [SerializeField] private float _kunaiSpeed;
        [SerializeField] private bool _mouseShoot;
        private Player _player;
        private Camera _mainCamera;
        
        private bool _canShoot = true;
        public bool HaveKunai { get; set; }
        public float CooldownTimer = 0.3f;//Max Level

        private void Start()
        {
            _player = GetComponent<Player>();
            _player.PlayerInputs.OnFireKunai += FireKunai;
            _mainCamera = Camera.main;
        }

        private void FireKunai()
        {
            if (_canShoot && HaveKunai)
            {
                StartCoroutine(ShootCooldown());
                ShootKunai();
            }
        }

        private IEnumerator ShootCooldown()
        {
            _canShoot = false;
            yield return new WaitForSeconds(CooldownTimer);
            _canShoot = true;
        }

        private void ShootKunai()
        {
            Vector3 direction = _player.PlayerInputs.SecondatyJoystickDirectionAligned;
            direction = direction == Vector3.zero ? transform.forward : direction;
            
            Rigidbody kunaiRigidbody = Instantiate(_kunai, _kunaiSpawnPosition.position,  Quaternion.identity).GetComponent<Rigidbody>();
            kunaiRigidbody.transform.SetParent(transform.parent);
            kunaiRigidbody.AddForce(direction.normalized * _kunaiSpeed, ForceMode.Impulse);
        }
    }
}
