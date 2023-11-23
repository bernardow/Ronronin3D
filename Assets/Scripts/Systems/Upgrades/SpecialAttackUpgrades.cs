using Units.Player;
using UnityEngine;

namespace Systems.Upgrades
{
    public class SpecialAttackUpgrades : MonoBehaviour, IUpgrade
    {
        private int _currentLevel;
        private Player _player;

        private void Awake() => _player = GameObject.FindWithTag("Player").GetComponent<Player>();

        public void LoadData(UpgradesData data) => _currentLevel = data.SpecialAttackUpgradeLevel;
    

        public void SetUpgradeData()
        {
            if (_player.PlayerAttack == null) return;

            switch (_currentLevel)
            {
                case 1: _player.PlayerAttack.AttackDamage = 100;
                    break;
                case 2: _player.PlayerAttack.SpecialCoolDownTimer = 25;
                    break;
                case 3: _player.PlayerAttack.SpecialCoolDownTimer = 20;
                    break;
                case 4: _player.PlayerAttack.AttackDamage = 150;
                    break;
                default: _player.PlayerAttack.AttackDamage = 50;
                    _player.PlayerAttack.SpecialCoolDownTimer = 30;
                    break;
            }
        }
    }
}
