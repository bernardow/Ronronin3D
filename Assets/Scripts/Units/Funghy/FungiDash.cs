using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Units.Funghy
{
    public class FungiDash : MonoBehaviour, IObserver
    {
        private Funghy _funghy;

        [SerializeField] private float _duration;
        [SerializeField] private float _dashForce;
        [SerializeField] private LayerMask _mask;
        private float _counter;
        private Vector3 _direction;
        private bool _isDashing;

        private void Awake()
        {
            _funghy = GetComponent<Funghy>();
        }

        private void Start()
        {
            _direction = new Vector3(Random.Range(0, 100), _funghy.FungiTransform.localPosition.y, Random.Range(0, 100));
        }
        
        private void Update()
        {
            if (_isDashing)
            {
                _counter += Time.deltaTime;
                _funghy.FungiTransform.position += _direction * _dashForce * Time.deltaTime;
                
                Vector3 leftPoint = Vector3.Cross(_direction, Vector3.up) * 2f;
                Vector3 rightPoint = -leftPoint;
                
                Debug.DrawRay(leftPoint + transform.localPosition, _direction * 2.5f, Color.red);
                Debug.DrawRay(rightPoint + transform.localPosition, _direction * 2.5f, Color.red);
                
            }

            if (_counter >= _duration)
            {
                _isDashing = false;
                _counter = 0;
                _funghy.RunStateMachine();
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.collider.CompareTag("Ground"))
            {
                _direction = FungiUtilities.ChangeDirection(_direction, FungiUtilities.ChangeTypes.CROSS);
                _direction.Normalize();
                SearchForWalls(_funghy.FungiTransform.localPosition);
                StartCoroutine(SecondSearch());
            }
        }

        private void SearchForWalls(Vector3 startPoint)
        {
            Vector3 leftPoint = Vector3.Cross(_direction, Vector3.up);
            Vector3 rightPoint = -leftPoint;
            
            Ray ray = new Ray(rightPoint + startPoint, _direction * 2.5f);
            if (Physics.Raycast(ray, out RaycastHit hit, 2.5f, _mask))
            {
                if (hit.collider.CompareTag("Setup"))
                {
                    _direction = -_direction;
                    return;
                }
            }
            
            Ray leftRay =  new Ray(leftPoint + startPoint, _direction * 2.5f);
            if (Physics.Raycast(leftRay, out RaycastHit hitInfo, 2.5f, _mask))
            {
                if (hitInfo.collider.CompareTag("Setup"))
                    _direction = -_direction;
            }
        }

        private IEnumerator SecondSearch()
        {
            yield return new WaitForSeconds(1f);
            SearchForWalls(_funghy.FungiTransform.localPosition);
            
        }

        private IEnumerator DashStartDelay()
        {
            yield return new WaitForSeconds(2f);
            _isDashing = true;
        }

        public void OnNotify()
        {
            _funghy.ManageIdleMovement();
            StartCoroutine(DashStartDelay());
        }

        public void Disable()
        {
            enabled = false;
        }

        public void Enable()
        {
            enabled = true;
        }
    }
}
