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

        [SerializeField] private GameObject gameOverScreen;

        public static GameManager Instance;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            
            Application.targetFrameRate = 90;
            //Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 1;
        }

        public void Quit_OnClick() => Application.Quit();

        public void ShowGameOverScreen()
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
