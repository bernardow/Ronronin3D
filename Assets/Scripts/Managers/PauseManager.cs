using System;
using Units;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class PauseManager : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseScreen;
        [SerializeField] private GameObject _optionsScreen;
        [SerializeField] private GameObject _gameOverScreen;

        private void Start()
        {
            //_playerUnit = GameObject.FindWithTag("Player").GetComponent<BaseUnit>();
            //BaseUnit.PlayerDeath += ShowGameOverScreen();
        }

        public void Pause()
        {
            _pauseScreen.SetActive(!_pauseScreen.activeSelf);
            Time.timeScale = Time.timeScale >= 1 ? 0 : 1;
        }

        public void Quit() => SceneManager.LoadScene(0);

        public void InGameOptions()
        {
            _pauseScreen.SetActive(!_pauseScreen.activeSelf);
            _optionsScreen.SetActive(!_optionsScreen.activeSelf);
        }

        public void ShowGameOverScreen()
        {
            _gameOverScreen.SetActive(!_gameOverScreen.activeSelf);
            Time.timeScale = Time.timeScale > 0 ? 0 : 1;
        }

        public void ChangeScene(int sceneIndex) => SceneManager.LoadScene(sceneIndex);
        

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton8))
                Pause();
        }

    }
}
