using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _bossPrefab;
        
        private void Awake()
        {
            Application.targetFrameRate = 90;
            //Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 1;
        }
    }
}
