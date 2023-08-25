using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class PauseManager : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseScreen;
        [SerializeField] private GameObject _optionsScreen;
        
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
    }
}
