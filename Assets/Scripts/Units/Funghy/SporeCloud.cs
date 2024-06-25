using System.Collections;
using UnityEngine;
using Utilities;

namespace Units.Funghy
{
    public class SporeCloud : MonoBehaviour, IObserver
    {
        [SerializeField] private float _scaleSpeed = 1;
        [SerializeField] private float _maxSize = 3.5f;
        [SerializeField] private AnimationCurve _evaluationCurve;
        [SerializeField] private Transform _sporeCloud;
        [SerializeField] private float _duration = 3;

        private float _current;
        private Vector3 _castPos, _goalPos;

        private Funghy _funghy;

        private bool _isExpanding;
        
        // Start is called before the first frame update
        void Start() => _funghy = GetComponent<Funghy>();

        // Update is called once per frame
        void Update()
        {
            if (_isExpanding)
            {
                _current = Mathf.MoveTowards(_current, 1, _scaleSpeed * Time.deltaTime);
                _sporeCloud.localScale = Vector3.Lerp(Vector3.one, new Vector3(_maxSize, _maxSize, _maxSize), _evaluationCurve.Evaluate(_current));
            }
        }

        public IEnumerator Run()
        {
            _funghy.ManageIdleMovement(false);
            _sporeCloud.localScale = Vector3.one / 2;
            SporeState(true);
            yield return new WaitForSeconds(_duration);
            _isExpanding = true;
            _current = 0;
            yield return new WaitForSeconds(_duration);
            _isExpanding = false;
            SporeState(false);
        }

        public void OnNotify()
        {
            StartCoroutine(Run());
        }

        public void Disable()
        {
            enabled = false;
            StopAllCoroutines();
        }

        public void Enable()
        {
            enabled = true;
        }

        public void SporeState(bool activationState) => _sporeCloud.gameObject.SetActive(activationState);
    }
}
