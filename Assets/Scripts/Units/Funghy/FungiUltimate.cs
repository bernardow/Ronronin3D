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
    [SerializeField] private float _widthMultiplier;

    [SerializeField] private Transform _fungiCenter;
    [SerializeField] private LineRenderer[] _lasers;
    
    private bool _shootLaser;
    private bool _canDealDamage;
    private Funghy _funghy;
    [SerializeField] private float _ultimateDamage;

    public event EventHandler<OnUltimateArgs> OnUltimateHit = delegate {  };
    
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
            
            ShootLaser(_lasers[0], forward);
            ShootLaser(_lasers[1], -forward);
            ShootLaser(_lasers[2], right);
            ShootLaser(_lasers[3], -right);
            ShootLaser(_lasers[4], forward + right);
            ShootLaser(_lasers[5], forward - right);
            ShootLaser(_lasers[6], -forward + right);
            ShootLaser(_lasers[7], -forward -right);
        }
        else foreach (LineRenderer laser in _lasers)
                laser.enabled = false;
    }

    private IEnumerator CastUltimate(float timer)
    {
        CanTurnVulnerable = true;
        _funghy.ManageIdleMovement();
        yield return new WaitForSeconds(timer);
        CanTurnVulnerable = false;
        StartCoroutine(StartUltimate(_ultimateDuration));
    }

    private IEnumerator StartUltimate(float timer)
    {
        _shootLaser = true;
        _canDealDamage = false;
        yield return new WaitForSeconds(timer * 0.33f);
        _canDealDamage = true;
        foreach (LineRenderer laser in _lasers)
        {
            DOVirtual.Float(laser.widthMultiplier, _widthMultiplier, 0.5f, t =>
            {
                laser.widthMultiplier = t;
            }).SetEase(Ease.InOutBounce);
        }
        
        yield return new WaitForSeconds(timer * 0.66f);
        _shootLaser = false;
        _funghy.RunStateMachine();
    }

    private void ShootLaser(LineRenderer laser, Vector3 direction)
    {
        Ray ray = new Ray(_fungiCenter.position, direction);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            laser.enabled = true;
            laser.SetPosition(0, transform.position);
            laser.SetPosition(1, hit.point);

            if (hit.collider.CompareTag("Player") && _canDealDamage)
                OnUltimateHit.Invoke(null, new OnUltimateArgs(){ Damage = _ultimateDamage});
        }
    }

    private void RotateCenter() => _fungiCenter.Rotate(Vector3.up, _rotationAnglesPerTime * Time.deltaTime);
    
    public void OnNotify() => StartCoroutine(CastUltimate(_castingTimer));
}
