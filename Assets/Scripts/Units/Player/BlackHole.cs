using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlackHole : MonoBehaviour
{
    [SerializeField] private float lifeTime = 10;
    
    [SerializeField] private float orbitSpeed = 50f;
    [SerializeField] private float minDistance = 3;
    public float MaxDistance = 15;

    private List<BlackHoleProjectile> projectilesInBlackHole;

    private void OnEnable()
    {
        projectilesInBlackHole = new List<BlackHoleProjectile>();
        StartCoroutine(RunLifeCycle());
    }

    private IEnumerator RunLifeCycle()
    {
        while (lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
            yield return null;
        }
        
        Explode();
        gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectiles"))
        {
            StartCoroutine(OrbitAround(other.gameObject));
        }
    }

    private IEnumerator OrbitAround(GameObject projectile)
    {
        float randomDistance = Random.Range(minDistance, MaxDistance);
        float distance = Vector3.Distance(transform.position, projectile.transform.position);
        
        while (distance > randomDistance && projectile != null)
        {
            distance = Vector3.Distance(transform.position, projectile.transform.position);
            yield return null;
        }

        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>(); 
        projectileRigidbody.useGravity = false;

        BlackHoleProjectile blackHoleProjectile = new BlackHoleProjectile
        {
            BlackHole = this,
            Projectile = projectileRigidbody,
            InUse = true,
            Velocity = projectileRigidbody.velocity
        };
        projectilesInBlackHole.Add(blackHoleProjectile);
        
        projectileRigidbody.velocity = Vector3.zero;

        while (projectile != null && blackHoleProjectile.InUse && distance < MaxDistance)
        {
            projectile.transform.RotateAround(transform.position, Vector3.up, orbitSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void Explode()
    {
        StopAllCoroutines();
        
        for (int i = 0; i < projectilesInBlackHole.Count; i++)
        {
            BlackHoleProjectile blackHoleProjectile = projectilesInBlackHole[i];
            
            if (!blackHoleProjectile.Available) continue;
            blackHoleProjectile.InUse = false;
            Vector3 direction = blackHoleProjectile.Projectile.transform.forward;
            direction.y = 0;
            blackHoleProjectile.Projectile.AddForce(direction.normalized * (orbitSpeed * 2), ForceMode.Impulse);
        }
        
        gameObject.SetActive(false);
    }
}

public class BlackHoleProjectile
{
    public Rigidbody Projectile;
    public BlackHole BlackHole;
    public Vector3 Velocity;
    public bool InUse;
    public bool Available => IsAvailable();

    private bool IsAvailable()
    {
        if (Projectile == null) return false;
        
        return Vector3.Distance(Projectile.transform.position, BlackHole.transform.position) < BlackHole.MaxDistance && Projectile != null;
    }
}
