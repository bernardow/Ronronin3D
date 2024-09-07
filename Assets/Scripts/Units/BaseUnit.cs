using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Units
{ 
    public class BaseUnit : MonoBehaviour
    {
        public float Life;
        public float InitialLife { get; private set; }
        public float Damage = 8;
        
        public bool IsAlive { get; private set; }

        public bool CanTakeDamage { get; set; } = true;

        private void Awake()
        {
            InitialLife = Life;
        }

        public void Spawn()
        {
            Life = InitialLife;
            CanTakeDamage = true;
            IsAlive = true;
        }

        public virtual void AddLife(float amount)
        {
            Life += amount;
        }

        public virtual void RemoveLife(float amount)
        {
            if(!CanTakeDamage)
                return;
            
            Life -= amount;
            CheckLife();
        }

        protected virtual void CheckLife()
        {
            if (Life <= 0)
            {
                Kill();
            }
                
        }
        
        public void Kill()
        {
            IsAlive = false;
            CanTakeDamage = false;
            gameObject.SetActive(false);
        }

        public void RemoveBossLife(int amount) => RemoveLife(amount);
    }
}
