using System.Collections;
using UnityEngine;
using Utilities;

namespace Units.Player
{
    public class KunaiAttack : MonoBehaviour
    {
        [SerializeField] private GameObject _kunai;
        [SerializeField] private float _kunaiSpeed;
        [SerializeField] private float _cooldownTimer;
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
            Vector3 goalPosition = Helpers.GetBossPosition() - _player.PlayerTransform.position;
            Rigidbody kunaiRigidbody = Instantiate(_kunai, _player.PlayerTransform).GetComponent<Rigidbody>();
            kunaiRigidbody.transform.SetParent(transform.parent);
            kunaiRigidbody.AddForce(goalPosition * _kunaiSpeed, ForceMode.Impulse);
        }
    }
}
