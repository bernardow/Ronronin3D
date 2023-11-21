using UnityEngine;

namespace Systems.Player_Death_Data
{
    public class PlayerDeathManager : MonoBehaviour
    {
        [SerializeField] private bool _debug;
        
        private void Awake()
        {
            if(_debug) PlayerPrefs.SetInt("death_count", 0);
            
            if (PlayerPrefs.HasKey("death_count")) return;
            WriteData();
        }

        public static void WriteData()
        {
            if (!PlayerPrefs.HasKey("death_count"))
                PlayerPrefs.SetInt("death_count", 0);
            else 
                PlayerPrefs.SetInt("death_count", GetData() + 1);
            
            PlayerPrefs.Save();
        }

        public static int GetData()
        {
            return PlayerPrefs.GetInt("death_count");
        }
    }
}
