using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    [SerializeField] private float lifetime = 4;
    [SerializeField] private float explosionForce = 25;
    private List<SunPlanet> projectilesInRange = new List<SunPlanet>();

    private void Start()
    {
        StartCoroutine(RunLifeCycle());
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.collider.CompareTag("Projectiles") && !other.collider.CompareTag("Boss") &&
            !other.collider.CompareTag("Boss")) return;
        
        Explode();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Projectiles") && !other.CompareTag("Boss") &&
            !other.CompareTag("Boss") && !other.CompareTag("Player")) return;

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
            !other.CompareTag("Boss") && !other.CompareTag("Player")) return;

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
            
            Vector3 direction = projectilesInRange[i].Planet.position - transform.position;
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
