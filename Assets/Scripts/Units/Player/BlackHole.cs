using System;
using System.Collections;
using System.Collections.Generic;
using Units;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlackHole : MonoBehaviour
{
    [SerializeField] private float damage;
    
    [SerializeField] private float lifeTime = 10;
    [SerializeField] private float gravityForce = 2;
    [SerializeField] private float orbitSpeed = 50f;
    [SerializeField] private float explosionForce = 75;
    
    [SerializeField] private float minDistance = 3;
    public float MaxDistance = 15;

    private Rigidbody rigidbody;
    private List<BlackHoleProjectile> projectilesInBlackHole;

    private void OnEnable()
    {
        projectilesInBlackHole = new List<BlackHoleProjectile>();
        rigidbody = GetComponent<Rigidbody>();
        StartCoroutine(RunLifeCycle());
    }

    private IEnumerator RunLifeCycle()
    {
        while (lifeTime > 0)
        {
            if (lifeTime <= 8f && !rigidbody.isKinematic)
                rigidbody.isKinematic = true;
            
            lifeTime -= Time.deltaTime;
            yield return null;
        }
        
        Explode();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectiles") || other.CompareTag("BlackHoleBlocks"))
        {
            StartCoroutine(OrbitAround(other.gameObject));
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.collider.CompareTag("Projectiles") && !other.collider.CompareTag("Boss") &&
            !other.collider.CompareTag("Boss") && !other.collider.CompareTag("Player") && !other.collider.CompareTag("BlackHoleBlocks")) return;

        if (other.collider.CompareTag("BlackHoleBlocks"))
            other.collider.GetComponent<Rigidbody>().isKinematic = false;
        
        if (other.gameObject.TryGetComponent(typeof(BaseUnit), out Component unit))
            StartCoroutine(DealDamage((BaseUnit)unit));
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
            Vector3 direction = (transform.position - projectile.transform.position).normalized;
            projectile.transform.position += direction * (gravityForce * Time.deltaTime);
            
            projectile.transform.RotateAround(transform.position, Vector3.up, orbitSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator DealDamage(BaseUnit unit)
    {
        float distance;
        
        do
        {
            unit.RemoveLife(damage);

            float timer = 0.0f;
            float totalTime = 1.5f;

            while (timer < totalTime)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            
            distance = Vector3.Distance(transform.position, unit.transform.position);
        } while (distance < 4);
    }

    public void Explode()
    {
        StopAllCoroutines();
        
        for (int i = 0; i < projectilesInBlackHole.Count; i++)
        {
            BlackHoleProjectile blackHoleProjectile = projectilesInBlackHole[i];
            
            if (!blackHoleProjectile.Available) continue;

            if (blackHoleProjectile.Projectile.CompareTag("BlackHoleBlocks"))
                blackHoleProjectile.Projectile.isKinematic = false;
            
            blackHoleProjectile.InUse = false;
            Vector3 direction = blackHoleProjectile.Projectile.transform.forward;
            direction.y = 0;
            blackHoleProjectile.Projectile.AddForce(direction.normalized * explosionForce, ForceMode.Impulse);
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
