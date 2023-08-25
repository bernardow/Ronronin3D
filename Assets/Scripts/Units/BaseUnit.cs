using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Units
{
    public class BaseUnit : MonoBehaviour
    {
        public float Life;
        public float InitialLife { get; private set; }
        public readonly float Damage = 8;

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
            
            Material healthBarMaterial = HealthBar.GetComponent<SpriteRenderer>().sharedMaterial;
            healthBarMaterial.SetFloat("_Life", Life / 100);
        }

        public void SelfDestroy() => Destroy(gameObject);
    }
}
