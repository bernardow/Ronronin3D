using System;
using System.Collections;
using System.Collections.Generic;
using Units;
using Units.Player;
using UnityEditor;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

public class MinionUnit : MonoBehaviour
{
    [SerializeField] private float _minionSpeed;
    [SerializeField] private float _lifeBonus = 5;
    
    private Transform _minionTransform;
    private BaseUnit _baseUnit;
    private Player _player;

    private void OnDestroy()
    {
        int randomNum = Random.Range(0, 5);
        if(randomNum == 1)
            _player.PlayerHealth.AddLife(_lifeBonus);
    }

    private  void  Start()
    {
        _minionTransform = transform;
        _baseUnit = GetComponent<BaseUnit>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        StartCoroutine(SetLifeTime(10f));
    }

    private void Update()
    {
        _minionTransform.position += (Helpers.GetPlayerPosition() - _minionTransform.position) * _minionSpeed * Time.deltaTime;
    }

    private IEnumerator SetLifeTime(float timer)
    {
        yield return new WaitForSeconds(timer);
        _baseUnit.SelfDestroy();
    }
}
