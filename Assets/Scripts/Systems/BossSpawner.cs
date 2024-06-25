using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject Boss;
    public Collider[] Colliders;
    public GameObject HealthBar;

    public static BossSpawner Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(gameObject);
    }

    public void SpawnBoss()
    {
        HealthBar.SetActive(true);
        Boss.SetActive(true);
        for (int i = 0; i < Colliders.Length; i++)
        {
            Colliders[i].GetComponent<Renderer>().enabled = true;
            Colliders[i].isTrigger = false;
        }
    }

    public void DisableColliders()
    {
        for (int i = 0; i < Colliders.Length; i++)
        {
            Colliders[i].GetComponent<Renderer>().enabled = false;
            Colliders[i].enabled = false;
        }

        HealthBar.SetActive(false);
    }
}
