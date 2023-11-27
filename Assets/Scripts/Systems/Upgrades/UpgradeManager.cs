using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Systems.Upgrades
{
    public class UpgradeManager : MonoBehaviour
    {
        public List<IUpgrade> Upgrades = new List<IUpgrade>();
        
        public event Action OnUpgradeBuy = delegate {  };
        
        private void Start()
        {
            if (!PlayerPrefs.HasKey("upgrades_data_created"))
            {
                PlayerPrefs.SetInt("upgrades_data_created", 1);
                string json = JsonUtility.ToJson(new UpgradesData());
                File.WriteAllText(Application.persistentDataPath + "/UpgradesData.json", json);
                PlayerPrefs.Save();
            }

            for (int i = 0; i < transform.childCount; i++)
            {
                IUpgrade upgrade = transform.GetChild(i).GetComponent<IUpgrade>();
                Upgrades.Add(upgrade);
            }
            
            UpgradesData data = GetUpgradesData();

            UpdateUpgrades(data);
        }

        public static UpgradesData GetUpgradesData()
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/UpgradesData.json");
            return JsonUtility.FromJson<UpgradesData>(json);
        }

        public void UpgradeSkill(int upgradeTypes)
        {
            UpgradesData data = GetUpgradesData();

            if (data.PlayerMoney < 100) return;
            
            switch (upgradeTypes)
            {
                case 0: data.GeneralUpgradeLevel++;
                    break;
                case 1: data.KunaiUpgradeLevel++;
                    break;
                case 2: data.MeleeUpgradeLevel++;
                    break;
                case 3: data.DashUpgradeLevel++;
                    break;
                case 4: data.SpecialAttackUpgradeLevel++;
                    break;
                default: throw new NullReferenceException("Upgrade type not found");
            }
            
            data.PlayerMoney -= 100;
            UpdateUpgrades(data);
            OverrideUpgradeDataJSON(data);
            OnUpgradeBuy.Invoke();
        }

        private void UpdateUpgrades(UpgradesData data)
        {
            foreach (IUpgrade upgrade in Upgrades)
            {
                upgrade.LoadData(data);
                upgrade.SetUpgradeData();
            }
        }

        public static void OverrideUpgradeDataJSON(UpgradesData data)
        {
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/UpgradesData.json", json);
        }
    }
}
