using System;
using System.Collections;
using Units.Funghy;
using UnityEngine;
using Utilities;

public class FungiUltimate : MonoBehaviour, IObserver
{
    public bool CanTurnVulnerable { get; private set; }

    [SerializeField] private float _castingTimer;
    [SerializeField] private float _ultimateDuration;
    [SerializeField] private float _rotationAnglesPerTime;

    public Transform _fungiCenter;
    
    public bool ShootLaser;
    private Funghy _funghy;
    public LaserAttack LaserAttack;
    
    private void Awake()
    {
        _funghy = GetComponent<Funghy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ShootLaser)
        {
            RotateCenter();
            Vector3 forward = _fungiCenter.forward;
            Vector3 right = _fungiCenter.right;
            Vector3 center = _fungiCenter.position;
            
            LaserAttack.ShootLaser(LaserAttack.Lasers[0], center,forward);
            LaserAttack.ShootLaser(LaserAttack.Lasers[1], center,-forward);
            LaserAttack.ShootLaser(LaserAttack.Lasers[2], center,right);
            LaserAttack.ShootLaser(LaserAttack.Lasers[3], center,-right);
            LaserAttack.ShootLaser(LaserAttack.Lasers[4], center,forward - right);
            LaserAttack.ShootLaser(LaserAttack.Lasers[5], center,forward + right);
            LaserAttack.ShootLaser(LaserAttack.Lasers[6], center,- forward + right);
            LaserAttack.ShootLaser(LaserAttack.Lasers[7], center,- forward - right);
        }
        else foreach (LineRenderer laser in LaserAttack.Lasers)
                laser.enabled = false;
    }

    public IEnumerator Run()
    {
        CanTurnVulnerable = true;
        Debug.Log("Casting Ultimate");
        if (_funghy == null)
            _funghy = GetComponent<Funghy>();
        //_funghy.ManageIdleMovement(false);
        yield return new WaitForSeconds(_castingTimer);
        CanTurnVulnerable = false;
        StartCoroutine(StartUltimate(_ultimateDuration));
        yield return new WaitForSeconds(_ultimateDuration);
        ShootLaser = false;
    }

    private IEnumerator StartUltimate(float timer)
    {
        ShootLaser = true;
        yield return new WaitForSeconds(timer * 0.33f);
        yield return StartCoroutine(LaserAttack.LaserBehaviour(timer));
    }

    public void RotateCenter() => _fungiCenter.Rotate(Vector3.up, _rotationAnglesPerTime * Time.deltaTime);
    public void RotateCenter(float angle) => _fungiCenter.Rotate(Vector3.up, angle);
    
    public void OnNotify() => RunUltimateRPC();

    public void RunUltimateRPC() => StartCoroutine(Run());
    
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void Disable()
    {
        enabled = false;
        ShootLaser = false;
        StopAllCoroutines();
    }

    public void Enable()
    {
        enabled = true;
    }
}
