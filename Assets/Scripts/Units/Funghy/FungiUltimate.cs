using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro.EditorUtilities;
using Units.Funghy;
using UnityEngine;
using Utilities;

public class FungiUltimate : MonoBehaviour, IObserver
{
    public bool CanTurnVulnerable { get; private set; }

    [SerializeField] private float _castingTimer;
    [SerializeField] private float _ultimateDuration;
    [SerializeField] private float _rotationAnglesPerTime;

    [SerializeField] private Transform _fungiCenter;
    
    private bool _shootLaser;
    private Funghy _funghy;
    public LaserAttack LaserAttack;
    
    private void Awake()
    {
        _funghy = GetComponent<Funghy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_shootLaser)
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

    private IEnumerator CastUltimate(float timer)
    {
        CanTurnVulnerable = true;
        _funghy.ManageIdleMovement(false);
        yield return new WaitForSeconds(timer);
        CanTurnVulnerable = false;
        StartCoroutine(StartUltimate(_ultimateDuration));
    }

    private IEnumerator StartUltimate(float timer)
    {
        _shootLaser = true;
        yield return new WaitForSeconds(timer * 0.33f);
        yield return StartCoroutine(LaserAttack.LaserBehaviour(timer));
        _shootLaser = false;
        _funghy.RunStateMachine();
    }

    private void RotateCenter() => _fungiCenter.Rotate(Vector3.up, _rotationAnglesPerTime * Time.deltaTime);
    
    public void OnNotify() => StartCoroutine(CastUltimate(_castingTimer));
    public void Disable()
    {
        enabled = false;
        _shootLaser = false;
        StopAllCoroutines();
    }

    public void Enable()
    {
        enabled = true;
    }
}
