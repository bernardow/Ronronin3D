using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Units
{
    //todo fix healthbar texture
    public class BaseUnit : MonoBehaviour
    {
        public float Life;
        public float InitialLife { get; private set; }
        public float Damage = 8;

        public bool CanTakeDamage { get; set; }

        private void Start()
        {
            InitialLife = Life;
            CanTakeDamage = true;
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
        }

        public virtual void CheckLife()
        {
            if (Life <= 0)
            {
                SelfDestroy();
            }
                
        }

        public void SelfDestroy() => gameObject.SetActive(false);
    }
}
