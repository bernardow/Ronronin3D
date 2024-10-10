using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    [SerializeField] private float lifetime = 4;
    [SerializeField] private float explosionForce = 25;
    private List<SunPlanet> projectilesInRange = new List<SunPlanet>();
    private Rigidbody rigidbody;
    
    private void Start()
    {
        StartCoroutine(RunLifeCycle());
        rigidbody = GetComponent<Rigidbody>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (!other.collider.CompareTag("Projectiles") && !other.collider.CompareTag("Boss") &&
            !other.collider.CompareTag("Boss") && !other.collider.CompareTag("SunBlocks")) return;
        
        Explode();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Projectiles") && !other.CompareTag("Boss") &&
            !other.CompareTag("Boss") && !other.CompareTag("Player") && !other.CompareTag("SunBlocks")) return;

        SunPlanet sunPlanet = new SunPlanet
        {
            Sun = this,
            Planet = other.GetComponent<Rigidbody>()
        };
        
        projectilesInRange.Add(sunPlanet);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Projectiles") && !other.CompareTag("Boss") &&
            !other.CompareTag("Boss") && !other.CompareTag("Player") && !other.CompareTag("SunBlocks")) return;

        for (int i = 0; i < projectilesInRange.Count; i++)
        {
            if (projectilesInRange[i].IsAvailable && projectilesInRange[i].Planet.transform == other.transform)
                projectilesInRange.Remove(projectilesInRange[i]);
        }
    }

    private IEnumerator RunLifeCycle()
    {
        while (lifetime > 0)
        {
            if (lifetime <= 1.5f && !rigidbody.isKinematic)
                rigidbody.isKinematic = true;
                
            lifetime -= Time.deltaTime;
            yield return null;
        }

        Explode();
    }

    private void Explode()
    {
        for (int i = 0; i < projectilesInRange.Count; i++)
        {
            if (!projectilesInRange[i].IsAvailable) continue;

            if (projectilesInRange[i].Planet.CompareTag("SunBlocks"))
                projectilesInRange[i].Planet.isKinematic = false;
            
            Vector3 direction = projectilesInRange[i].Planet.position - transform.position;
            direction.y = 0;
            projectilesInRange[i].Planet.AddForce(direction.normalized * explosionForce, ForceMode.Impulse);
        }
        
        Destroy(gameObject);
    }
}

public class SunPlanet
{
    public Rigidbody Planet;
    public Sun Sun;
    public bool IsAvailable => Available();

    private bool Available()
    {
        return Planet != null;
    }
}
