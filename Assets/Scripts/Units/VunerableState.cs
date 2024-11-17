using System.Collections;
using UnityEngine;

namespace Units
{
    public class VulnerableState : MonoBehaviour
    {
        [SerializeField] private float _timer;
        private IBoss _boss;
        private SpecialUnit _baseUnit;
        public bool InVulnerableState { get; private set; }

        private void Awake()
        {
            _boss = GetComponent<IBoss>();
            _baseUnit = GetComponent<SpecialUnit>();
        }

        public void RunVulnerableState()
        {
            StartCoroutine(_boss.StopStateMachine());
            StartCoroutine(SetVulnerableState(_timer));
        }

        private IEnumerator SetVulnerableState(float timer)
        {
            _baseUnit.DamageMultiplier = 2;
            InVulnerableState = true;
            yield return new WaitForSeconds(timer);
            InVulnerableState = false;
            _baseUnit.DamageMultiplier = 1;
            _boss.RunStateMachine();
        }
    }
}
