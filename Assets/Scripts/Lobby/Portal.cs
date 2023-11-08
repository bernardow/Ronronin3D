using System;
using Units.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lobby
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private PortalType _portalType;
        private Player _player;
    
        private enum PortalType
        {
            NewGame,
            NewRoom
        }
    
        public event Action OnNewGameStarted = delegate {  };  
        public event Action OnNewRoomEntered = delegate {  };

        private void Awake()
        {
            _player = GameObject.FindWithTag("Player").GetComponent<Player>();
            OnNewGameStarted += StartNewGame;
        }

        private void OnDestroy() => OnNewGameStarted -= StartNewGame;

        private void OnTriggerStay(Collider other)
        {
            if (!_player.PlayerInputs.InteractKeyboard && !_player.PlayerInputs.InteractJoystick) return;
            
            switch (_portalType)
            {
                case PortalType.NewGame: OnNewGameStarted.Invoke();
                    break;
                case PortalType.NewRoom: OnNewRoomEntered.Invoke();
                    break;
                default: throw new NullReferenceException("Portal type not found");
            }
        }

        private void StartNewGame()
        {
            SceneManager.LoadScene(1);
        }
    }
}
