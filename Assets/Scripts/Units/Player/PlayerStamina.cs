using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public static PlayerStamina Instance;
    public float Stamina = 100;
    [SerializeField] private float baseStaminaGain = 15;
    [SerializeField] private Material staminaMaterial;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(gameObject);
        
        staminaMaterial.SetFloat("_Life", 1);
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            Stamina = 99999;
    }
#endif

    public void AddStamina(float amount = 0)
    {
        float currentAmount = amount == 0 ? baseStaminaGain : amount;
        Stamina = Mathf.Min(Stamina + currentAmount, 100);
        
        staminaMaterial.SetFloat("_Life", Stamina / 100);
    }

    public void RemoveStamina(float amount)
    {
        Stamina = Mathf.Max(Stamina - amount, 0);
        staminaMaterial.SetFloat("_Life", Stamina / 100);
    }
}
