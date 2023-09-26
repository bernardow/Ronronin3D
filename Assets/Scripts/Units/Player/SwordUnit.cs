using System;
using System.Collections;
using UnityEngine;

namespace Units.Player
{
    public class SwordUnit : MonoBehaviour
    {
        [SerializeField] private float _damage;
        private Transform _swordTransform;
        private Collider _swordCollider;
        private MeshRenderer _meshRenderer;
        private Player _player;

        private void Awake()
        {
            _swordCollider = GetComponent<Collider>();
            _meshRenderer = GetComponent<MeshRenderer>();
            _player = GameObject.Find("Player").GetComponent<Player>();
            _swordTransform = transform;
        }

        private void Start() => _player.PlayerInputs.OnBasicAttack += StartAttack;

        private void OnDestroy() => _player.PlayerInputs.OnBasicAttack -= StartAttack;

        private void StartAttack() => StartCoroutine(StartAttackCoroutine());

        private void Update()
        {
            Vector3 desiredPosition = _player.PlayerTransform.localPosition + _player.PlayerTransform.forward * 2;
            _swordTransform.SetPositionAndRotation(desiredPosition, _player.PlayerTransform.localRotation);
        }

        private IEnumerator StartAttackCoroutine()
        {
            EnableCollider();
            _meshRenderer.enabled = true;
            yield return new WaitForSeconds(0.5f);
            DisableCollider();
            _meshRenderer.enabled = false;
        }

        private void EnableCollider() => _swordCollider.enabled = true;

        private void DisableCollider() => _swordCollider.enabled = false;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Boss") || other.CompareTag("Projectiles"))
            {
                BaseUnit baseUnit = other.GetComponent<BaseUnit>()!;
                baseUnit.RemoveLife(_damage);
            }
        }
    }
}
