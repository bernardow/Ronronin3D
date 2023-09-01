using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Units
{
    public class BaseUnit : MonoBehaviour
    {
        public float Life;
        public float InitialLife { get; private set; }
        public float Damage = 8;

        public bool HasHealthBar;
        [HideInInspector] public GameObject HealthBar;

        private void Start()
        {
            InitialLife = Life;
            UpdateHealthBar();
        }

        public void AddLife(float amount)
        {
            Life += amount;
            UpdateHealthBar();
        }

        public void RemoveLife(float amount)
        {
            Life -= amount;
            UpdateHealthBar();
            CheckLife();
            
            IBoss boss = GetComponent<IBoss>();
            boss?.PhaseChecker();
        }

        private void CheckLife()
        {
            if(Life <= 0)
                SelfDestroy();
        }

        private void UpdateHealthBar()
        {
            if(!HasHealthBar)
                return;

            Material healthBarMaterial = HealthBar.GetComponent<Image>().material;
            healthBarMaterial.SetFloat("_Life", Life / InitialLife);
        }

        public void SelfDestroy() => gameObject.SetActive(false);
    }
}
