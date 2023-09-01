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

        private IEnumerator StartCloudExpansion(float timer)
        {
            _funghy.ManageIdleMovement();
            _sporeCloud.localScale = Vector3.one / 2;
            _sporeCloud.gameObject.SetActive(true);
            yield return new WaitForSeconds(timer);
            _isExpanding = true;
            _current = 0;
            yield return new WaitForSeconds(timer);
            _isExpanding = false;
            _sporeCloud.gameObject.SetActive(false);
            _funghy.RunStateMachine();
            _funghy.ManageIdleMovement();
        }

        public void OnNotify()
        {
            StartCoroutine(StartCloudExpansion(3));
        }
    }
}
