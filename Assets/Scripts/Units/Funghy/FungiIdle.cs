using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Units.Funghy;
using UnityEngine;
using Utilities;
using static FungiUtilities;
using Random = UnityEngine.Random;

public class FungiIdle : MonoBehaviour
{
    [SerializeField] private float _moveForce;
    [SerializeField] private int _directionTimer;
    [SerializeField] private ChangeTypes _changeType;
    [SerializeField] private LayerMask _mask;
    public bool Deactivate;
    private int _initialTimer;
    private Vector3 _currentDirection;
    private Funghy _funghy;
    
    private void Start()
    {
        _initialTimer = _directionTimer;
        _funghy = GetComponent<Funghy>();
        _currentDirection = new Vector3(0.5f, _funghy.transform.position.y, -0.5f);
        StartCoroutine(DirectionTimer());

    }
    private void Update()
    {
        if(Deactivate)
            _funghy.FungiTransform.localPosition += _currentDirection * _moveForce * Time.deltaTime;
    }

  
    private IEnumerator DirectionTimer()
    {
        while (!Deactivate)
        {
            yield return StartCoroutine(ChangeDirectionTask(_directionTimer));
        }
    }

    private IEnumerator ChangeDirectionTask(int timer)
    {
        yield return new WaitForSeconds(timer);
        _currentDirection = ChangeDirection(_currentDirection, _changeType);
        _currentDirection = Helpers.SearchForWalls(_funghy.FungiTransform.localPosition, _currentDirection, _mask);
    }
    
    private IEnumerator SecondSearch()
    {
        yield return new WaitForSeconds(1f);
        _currentDirection = Helpers.SearchForWalls(_funghy.FungiTransform.localPosition, _currentDirection, _mask);
            
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.collider.CompareTag("Ground") && !other.collider.CompareTag("Projectiles"))
        {
            _directionTimer = _initialTimer;
            _currentDirection = ChangeDirection(_currentDirection, ChangeTypes.CROSS);
            _currentDirection.Normalize();
            _currentDirection = Helpers.SearchForWalls(_funghy.FungiTransform.localPosition, _currentDirection, _mask);
            StartCoroutine(SecondSearch());
        }
    }
}
