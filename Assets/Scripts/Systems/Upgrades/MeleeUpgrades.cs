using Units.Player;
using UnityEngine;

namespace Systems.Upgrades
{
    public class MeleeUpgrades : MonoBehaviour, IUpgrade
    {
        private int _currentLevel;
        [SerializeField] private SwordUnit _swordUnit;

    
        public void LoadData(UpgradesData data) => _currentLevel = data.MeleeUpgradeLevel;

        public void SetUpgradeData()
        {
            if (_swordUnit == null) return;

            
            switch (_currentLevel)
            {
                case 1: _swordUnit.SwordDamage = 12.5f;
                    break;
                case 2: _swordUnit.SwordDamage = 15;
                    break;
                case 3: _swordUnit.SwordDamage = 25;
                    break;
                case 4: _swordUnit.SwordDamage = 50;
                    break;
                default: _swordUnit.SwordDamage = 10;
                    break;
            }
        }
    }
}
