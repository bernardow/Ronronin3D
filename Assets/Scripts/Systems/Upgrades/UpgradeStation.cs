using System;
using Units.Player;
using UnityEngine;

namespace Systems.Upgrades
{
    public class UpgradeStation : MonoBehaviour
    {
        [SerializeField] private GameObject _upgradeScreen;
        private bool _canInteract;
        private Player _player;

        private void Start()
        {
            _player = GameObject.FindWithTag("Player").GetComponent<Player>();
            _player.PlayerInputs.OnInteractPressed += OpenUpgradeStation;
        }

        private void OnDestroy() => _player.PlayerInputs.OnInteractPressed -= OpenUpgradeStation;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                _canInteract = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
                _canInteract = false;
        }

        private void OpenUpgradeStation()
        {
            if (!_canInteract) return;
            _upgradeScreen.SetActive(true);
        }

        public void CloseUpgradeTab() => _upgradeScreen.SetActive(!_upgradeScreen.activeSelf);
    }
}
