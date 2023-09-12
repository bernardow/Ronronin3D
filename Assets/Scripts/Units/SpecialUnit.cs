using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Units
{
    public class SpecialUnit : BaseUnit
    {
        [SerializeField] private GameObject _healthbar;
        public event Action PlayerDeath = delegate {  };

        private void Awake() => UpdateHealthBar();

        public override void AddLife(float amount)
        {
            base.AddLife(amount);
            UpdateHealthBar();
        }

        public override void RemoveLife(float amount)
        {
            base.RemoveLife(amount);
            UpdateHealthBar();
            CheckLife();
            
            IBoss boss = GetComponent<IBoss>();
            boss?.PhaseChecker();

            if (tag == "Player")
                StartCoroutine(Invincibility());
        }

        public override void CheckLife()
        {
            if (Life <= 0)
            {
                SelfDestroy();
                PlayerDeath.Invoke();
            }
        }

        private void UpdateHealthBar()
        {
            Material healthBarMaterial = _healthbar.GetComponent<Image>().material;
            healthBarMaterial.SetFloat("_Life", Life / InitialLife);
        }
    
        private IEnumerator Invincibility()
        {
            Material blinkMat = GetComponent<MeshRenderer>().sharedMaterial;
            blinkMat.SetInt("_Blink", 1);
            CanTakeDamage = false;
            yield return new WaitForSeconds(1f);
            blinkMat.SetInt("_Blink", 0);
            CanTakeDamage = true;
        }
    }
}
