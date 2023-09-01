using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Units
{
    //todo fix healthbar texture
    public class BaseUnit : MonoBehaviour
    {
        public float Life;
        public float InitialLife { get; private set; }
        public float Damage = 8;

        public bool HasHealthBar;
        [HideInInspector] public GameObject HealthBar;

        private bool _canTakeDamage = true;

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
            if(!_canTakeDamage)
                return;
            
            Life -= amount;
            UpdateHealthBar();
            CheckLife();
            
            IBoss boss = GetComponent<IBoss>();
            boss?.PhaseChecker();

            if (tag == "Player")
                StartCoroutine(Invincibility());
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

        private IEnumerator Invincibility()
        {
            Material blinkMat = GetComponent<MeshRenderer>().sharedMaterial;
            blinkMat.SetInt("_Blink", 1);
            _canTakeDamage = false;
            yield return new WaitForSeconds(1f);
            blinkMat.SetInt("_Blink", 0);
            _canTakeDamage = true;
        }
    }
}
