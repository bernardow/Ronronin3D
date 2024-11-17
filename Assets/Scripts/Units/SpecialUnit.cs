using System;
using System.Collections;
using Systems.Player_Death_Data;
using Systems.Upgrades;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Units
{
    public class SpecialUnit : BaseUnit
    {
        public GameObject Healthbar;
        public float DamageMultiplier { get; set; }
        public event Action OnPlayerDeath = delegate {  };
        public event Action OnBossDeath = delegate {  };

        private void Start()
        {
            DamageMultiplier = 1;
            UpdateHealthBar();
            OnPlayerDeath += PlayerDeathManager.WriteData;
            OnPlayerDeath += AddPlayerDeathPoints;
            OnPlayerDeath += ReloadScene;
            OnBossDeath += AddBossDeathPoints;
            OnBossDeath += BossSpawner.Instance.DisableColliders;
        }

        private void OnDestroy()
        {
            OnPlayerDeath -= PlayerDeathManager.WriteData;
            OnPlayerDeath -= AddPlayerDeathPoints;
            OnPlayerDeath -= ReloadScene;
            OnBossDeath -= AddBossDeathPoints;
            OnBossDeath -= BossSpawner.Instance.DisableColliders;
        }

        public override void AddLife(float amount)
        {
            base.AddLife(amount);
            UpdateHealthBar();
        }

        public override void RemoveLife(float amount)
        {
            base.RemoveLife(amount * DamageMultiplier);
            UpdateHealthBar();
            CheckLife();
            
            IBoss boss = GetComponent<IBoss>();
            boss?.PhaseChecker();

            if (tag == "Player" && gameObject.activeSelf)
                StartCoroutine(Invincibility());
        }

        protected override void CheckLife()
        {
            if (!(Life <= 0)) return;
            
            Kill();
            if (tag == "Player")
            {
                OnPlayerDeath.Invoke();
                return;
            }
            OnBossDeath.Invoke();
        }

        private void UpdateHealthBar()
        {
            Material healthBarMaterial = Healthbar.GetComponent<Image>()?.material;
            healthBarMaterial!.SetFloat("_Life", Life / InitialLife);
        }
    
        private IEnumerator Invincibility()
        {
            //Material blinkMat = GetComponent<MeshRenderer>().sharedMaterial;
            //blinkMat!.SetInt("_Blink", 1);
            CanTakeDamage = false;
            yield return new WaitForSeconds(1f);
            //blinkMat!.SetInt("_Blink", 0);
            CanTakeDamage = true;
        }

        private void AddPlayerDeathPoints()
        {
            UpgradesData data = UpgradeManager.GetUpgradesData();
            data.PlayerMoney += 25;
            UpgradeManager.OverrideUpgradeDataJSON(data);
        }

        private void ReloadScene() => SceneManager.LoadScene(0);
        
        private void AddBossDeathPoints()
        {
            UpgradesData data = UpgradeManager.GetUpgradesData();
            data.PlayerMoney += 75;
            UpgradeManager.OverrideUpgradeDataJSON(data);
        }
    }
}
