using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BlockingDoor : MonoBehaviour
{
    private Rigidbody rigidbody;
    private bool startedDissolve;

    public static readonly int DissolveID = Shader.PropertyToID("_DissolveMeter");
    private MaterialPropertyBlock _materialPropertyBlock;
    private Renderer renderer;

    public MaterialPropertyBlock MaterialProps
    {
        get
        {
            if (_materialPropertyBlock == null)
                _materialPropertyBlock = new MaterialPropertyBlock();

            return _materialPropertyBlock;
        }
    }
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (!rigidbody.isKinematic && !startedDissolve)
        {
            StartCoroutine(Dissolve());
        }
    }

    private IEnumerator Dissolve()
    {
        float current = -1;
        startedDissolve = true;
        
        while (current < 1)
        {
            current += Time.deltaTime;
            MaterialProps.SetFloat(DissolveID, current);
            renderer.SetPropertyBlock(MaterialProps);
            yield return null;
        }

        Destroy(gameObject);
    }
}
