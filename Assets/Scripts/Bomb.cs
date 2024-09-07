using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Bomb : MonoBehaviour
{
    private Renderer renderer;
    private SphereCollider collider;
    private Rigidbody rigidbody;


    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        collider = GetComponent<SphereCollider>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);

        collider.enabled = true;
    }

    private IEnumerator Explode()
    {
        renderer.enabled = false;

        float totalTime = 1.0f;
        float timeElapsed = 0.0f;

        float colliderSize = collider.radius;
        //Bounds bounds = collider.bounds;

        collider.isTrigger = true;
        rigidbody.isKinematic = true;


        while (timeElapsed < totalTime)
        {
            timeElapsed += Time.deltaTime;
            float t = Mathf.Clamp01(timeElapsed / totalTime);
            collider.radius = Mathf.Lerp(colliderSize, colliderSize * 4, t);

            yield return null;
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player")) return;

        StartCoroutine(Explode());
    }
}
