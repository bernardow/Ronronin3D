using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LaserAttack : MonoBehaviour
{
    public float Damage { get; private set; }
    public LineRenderer[] Lasers = new LineRenderer[8];
    [SerializeField] private float _widthMultiplier;
    private bool _canDealDamage;

    public event EventHandler<OnUltimateArgs> OnUltimateHit = delegate(object sender, OnUltimateArgs args) {  }; 


    public IEnumerator LaserBehaviour(float timer)
    {
        _canDealDamage = true;
        foreach (LineRenderer laser in Lasers)
        {
            DOVirtual.Float(laser.widthMultiplier, _widthMultiplier, 0.5f, t =>
            {
                laser.widthMultiplier = t;
            }).SetEase(Ease.InOutBounce);
        }

        yield return new WaitForSeconds(timer * .66f);
        foreach (LineRenderer laser in Lasers)
            laser.widthMultiplier /= _widthMultiplier;
        _canDealDamage = false;
    }
    
    public void ShootLaser(LineRenderer laser, Vector3 origin,Vector3 direction)
        {
            Ray ray = new Ray(origin, direction);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                laser.enabled = true;
                laser.SetPosition(0, transform.position);
                laser.SetPosition(1, hit.point);
    
                if (hit.collider.CompareTag("Player") && _canDealDamage)
                    OnUltimateHit.Invoke(null, new OnUltimateArgs(){ Damage = Damage});
            }
        }
}
