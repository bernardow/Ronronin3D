using Units.Player;
using UnityEngine;

namespace Systems.Upgrades
{
    public class KunaiUpgrades : MonoBehaviour, IUpgrade
    {
        private int _currentLevel;
        private Player _player;

        //private void Awake() => _player = GameObject.FindWithTag("Player").GetComponent<Player>();

        public void LoadData(UpgradesData data)
        {
            _currentLevel = data.KunaiUpgradeLevel;
        }

        public void SetUpgradeData()
        {
            if (_player.PlayerKunaiAttack == null) return;
            
            /*
             * Temporary
             */
            int level = 3;
            switch (level)
            {
                case 1: 
                    _player.PlayerKunaiAttack.HaveKunai = true;
                    _player.PlayerKunaiAttack.CooldownTimer = 0.6f;
                    break;
                case 2:
                    _player.PlayerKunaiAttack.HaveKunai = true;
                    _player.PlayerKunaiAttack.CooldownTimer = 0.45f;
                    break;
                case 3: 
                    _player.PlayerKunaiAttack.HaveKunai = true;
                    _player.PlayerKunaiAttack.CooldownTimer = 0.3f;
                    break;
                case 4: 
                    _player.PlayerKunaiAttack.HaveKunai = true;
                    _player.PlayerKunaiAttack.CooldownTimer = 0.15f;
                    break;
                default: _player.PlayerKunaiAttack.HaveKunai = false;
                    break;
            }
        }
        public void SetPlayer(Player player) => _player = player;
    }
}
