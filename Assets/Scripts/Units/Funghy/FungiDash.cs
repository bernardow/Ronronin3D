using System.Collections;
using UnityEngine;
using Utilities;

namespace Units.Funghy
{
    public class FungiDash : MonoBehaviour, IObserver
    {
        private Funghy _funghy;
        
        private float _current;
        private Vector3 _castPos, _goalPos;
        [SerializeField] private AnimationCurve _evaluationCurve;
        [SerializeField] private float _castingTime = 2f;
        [SerializeField] private float _dashSpeed = 5f;

        private bool _isAttacking;

        private void Start() => _funghy = GetComponent<Funghy>();

        private void Update()
        {
            if (_isAttacking)
            {
                _current = Mathf.MoveTowards(_current, 1, _dashSpeed * Time.deltaTime);
                transform.position = Vector3.Lerp(_castPos, _goalPos, _evaluationCurve.Evaluate(_current));
            }
        }

        private IEnumerator StartDash(float castTime)
        {
            _goalPos = Helpers.GetPlayerPosition();
            yield return new WaitForSeconds(castTime);
            _isAttacking = true;
            _castPos = transform.position;
            
            _current = 0;
            yield return new WaitForSeconds(castTime);
            _isAttacking = false;
            _funghy.RunStateMachine();
        }
        
        public void OnNotify()
        {
            StartCoroutine(StartDash(_castingTime));
        }
    }
}
