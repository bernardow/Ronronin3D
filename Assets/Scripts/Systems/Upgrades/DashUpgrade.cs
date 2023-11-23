using Units.Player;
using UnityEngine;

namespace Systems.Upgrades
{
    public class DashUpgrade : MonoBehaviour, IUpgrade
    {
        private int _currentLevel;
        private Player _player;

        private void Awake() => _player = GameObject.FindWithTag("Player").GetComponent<Player>();

        public void LoadData(UpgradesData data) => _currentLevel = data.DashUpgradeLevel;

        public void SetUpgradeData()
        {
            if (_player.PlayerDash == null) return;

            switch (_currentLevel)
            {
                case 1:
                    _player.PlayerDash.HaveDash = true;
                    _player.PlayerDash.DashImpulse = 4;
                    break;
                case 2:
                    _player.PlayerDash.DashImpulse = 8;
                    break;
                case 3: 
                    _player.PlayerDash.DashImpulse = 12;
                    break;
                case 4:
                    _player.PlayerDash.DashCooldown = 0.5f;
                    break;
                default:
                    _player.PlayerDash.HaveDash = false;
                    break;
            }
        }
    }
}
