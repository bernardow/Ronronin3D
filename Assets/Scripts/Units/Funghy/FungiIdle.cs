using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Units.Funghy;
using UnityEngine;
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
            _currentDirection = ChangeDirection(_currentDirection, ChangeTypes.CROSS);
            _directionTimer = _initialTimer;
        }
    }

    private Vector3 ChangeDirection(Vector3 currentDirection, ChangeTypes changeType)
    {
        Vector3 newDirection = currentDirection;
        
        switch (changeType)
        {
            case ChangeTypes.CROSS:
                newDirection = Vector3.Cross(currentDirection, Vector3.up);
                break;
            case ChangeTypes.RANDOM:
                newDirection = new Vector3(Random.Range(0, 100), 0 ,Random.Range(0, 1));
                break;
            case ChangeTypes.ANGLE:
                float randomAngle = Random.Range(-180, 180);
                randomAngle = randomAngle * (Mathf.PI / 180);
                newDirection.x = currentDirection.magnitude * Mathf.Cos(randomAngle);
                newDirection.z = currentDirection.magnitude * Mathf.Sin(randomAngle);
                break;
        }

        return newDirection;
    }

    private enum ChangeTypes
    {
        CROSS,
        RANDOM,
        ANGLE
    }
}
