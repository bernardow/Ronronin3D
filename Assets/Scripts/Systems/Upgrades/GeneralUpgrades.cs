using Units.Player;
using UnityEngine;

namespace Systems.Upgrades
{
    public class GeneralUpgrades : MonoBehaviour, IUpgrade
    {
        private int _currentLevel;
        private Player _player;

        private void Awake() => _player = GameObject.FindWithTag("Player").GetComponent<Player>();

        public void LoadData(UpgradesData data) => _currentLevel = data.GeneralUpgradeLevel;

        public void SetUpgradeData()
        {
            switch (_currentLevel)
            {
                case 1: _player.PlayerMovement.PlayerSpeed = 110;
                    break;
                case 2: _player.PlayerMovement.PlayerSpeed = 120;
                    break;
                case 3: _player.PlayerMovement.PlayerSpeed = 135;
                    break;
                case 4: _player.PlayerMovement.PlayerSpeed = 150;
                    break;
                default: _player.PlayerMovement.PlayerSpeed = 100;
                    break;
            }
        }
    }
}
