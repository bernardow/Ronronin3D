using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Units.Funghy;
using UnityEngine;
using static FungiUtilities;
using Random = UnityEngine.Random;

public class FungiIdle : MonoBehaviour
{
    [SerializeField] private float _moveForce;
    [SerializeField] private int _directionTimer;
    [SerializeField] private ChangeTypes _changeType;
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
        _funghy.FungiTransform.position += _currentDirection * _moveForce * Time.deltaTime;
    }

  
    private IEnumerator DirectionTimer()
    {
        while (enabled)
        {
            yield return ChangeDirectionTask(_directionTimer);
        }
    }

    private IEnumerator ChangeDirectionTask(int timer)
    {
        yield return new WaitForSeconds(timer);
        _currentDirection = ChangeDirection(_currentDirection, _changeType);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.collider.CompareTag("Ground") && !other.collider.CompareTag("Projectiles"))
        {
            _directionTimer = _initialTimer;
        }
    }
}
