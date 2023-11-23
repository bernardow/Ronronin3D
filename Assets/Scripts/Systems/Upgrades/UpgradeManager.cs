using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Systems.Upgrades
{
    public class UpgradeManager : MonoBehaviour
    {
        public List<IUpgrade> Upgrades = new List<IUpgrade>();
    
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

            foreach (IUpgrade upgrade in Upgrades)
            {
                upgrade.LoadData(data);
                upgrade.SetUpgradeData();
            }
        }

        public UpgradesData GetUpgradesData()
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/UpgradesData.json");
            return JsonUtility.FromJson<UpgradesData>(json);
        }
    }
}
