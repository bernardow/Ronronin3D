using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 1;
        }
    }
}
