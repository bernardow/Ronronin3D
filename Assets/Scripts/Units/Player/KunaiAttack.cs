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
        [SerializeField] private float _cooldownTimer;
        [SerializeField] private bool _mouseShoot;
        private Player _player;

        private bool _canShoot = true;

        private void Start()
        {
            _player = GetComponent<Player>();
            _player.PlayerInputs.OnFireKunai += FireKunai;
        }

        private void FireKunai()
        {
            if (_canShoot)
            {
                StartCoroutine(ShootCooldown());
                ShootKunai();
            }
        }

        private IEnumerator ShootCooldown()
        {
            _canShoot = false;
            yield return new WaitForSeconds(_cooldownTimer);
            _canShoot = true;
        }

        private void ShootKunai()
        {
            Vector3 goalPosition = _mouseShoot? ShootKunaiRaycast() - _player.PlayerTransform.localPosition : Helpers.GetBossPosition() - _player.PlayerTransform.position;
            goalPosition = new Vector3(goalPosition.x, _player.PlayerTransform.position.y, goalPosition.z);
            Rigidbody kunaiRigidbody = Instantiate(_kunai, _kunaiSpawnPosition.position,  Quaternion.identity, _player.PlayerTransform).GetComponent<Rigidbody>();
            kunaiRigidbody.transform.SetParent(transform.parent);
            kunaiRigidbody.AddForce(goalPosition.normalized * _kunaiSpeed, ForceMode.Impulse);
        }

        private Vector3 ShootKunaiRaycast()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                return hitInfo.point;
            }

            throw new NullReferenceException("Shooting out of the screen");
        }
    }
}
