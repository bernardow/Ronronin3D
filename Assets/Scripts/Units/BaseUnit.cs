using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Units
{
    //todo fix healthbar texture
    public class BaseUnit : MonoBehaviourPunCallbacks
    {
        public float Life;
        public float InitialLife { get; private set; }
        public float Damage = 8;

        public bool CanTakeDamage { get; set; } = true;

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
            CheckLife();
        }

        public virtual void CheckLife()
        {
            if (Life <= 0)
            {
                CallSelfDestroy();
            }
                
        }
        
        public void CallSelfDestroy() => photonView.RPC("SelfDestroy", RpcTarget.All);

        public void CallBossRemoveRPC(int amount) => photonView.RPC("RemoveBossLife", RpcTarget.All, amount);
        
        [PunRPC]
        public void SelfDestroy() => gameObject.SetActive(false);

        [PunRPC]
        public void RemoveBossLife(int amount) => RemoveLife(amount);
    }
}
