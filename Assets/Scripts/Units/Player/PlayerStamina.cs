using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public static PlayerStamina Instance;
    public float Stamina = 100;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(gameObject);
    }

    public void AddStamina(float amount) => Stamina = Mathf.Min(Stamina + amount, 100);
    public void RemoveStamina(float amount) => Stamina = Mathf.Max(Stamina - amount, 0);
}
