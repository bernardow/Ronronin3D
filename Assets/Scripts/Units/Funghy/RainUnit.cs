using System.Collections;
using UnityEngine;

namespace Units.Funghy
{
    public class RainUnit : MonoBehaviour
    {
        [SerializeField] private float _dropSpeed;
        [SerializeField] private AnimationCurve _evaluationCurve;
        [SerializeField] private GameObject _drop;

        private float _current = 0;

        private Vector3 _castScale, _goalScale;
        
        // Start is called before the first frame update
        void Start()
        {
            _castScale = new Vector3(0.1f, 0.04f, 0.1f);
            _goalScale = new Vector3(1, 0, 1);
            StartCoroutine(SpawnDrop());
        }

        private IEnumerator SpawnDrop()
        {
            Instantiate(_drop, transform.position + Vector3.up * 8, Quaternion.identity);
            yield return new WaitForSeconds(1 / _dropSpeed);
            Destroy(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            _current = Mathf.MoveTowards(_current, 1, _dropSpeed * Time.deltaTime);
            transform.localScale = Vector3.Lerp(_castScale, _goalScale, _evaluationCurve.Evaluate(_current));
        }
    }
}
