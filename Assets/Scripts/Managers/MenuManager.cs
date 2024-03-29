using System;
using System.Collections.Generic;
using DG.Tweening;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject _roomScreen;
        [SerializeField] private TextMeshProUGUI _roomName;
        [SerializeField] private Button _playButton;
        [SerializeField] private GameObject[] _playersIcons;
        
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] private GameObject _optionsScreen;
        [SerializeField] private GameObject _menuScreen;
        [SerializeField] private GameObject _lobbyScreen;
        [SerializeField] private RectTransform _pointer;

        [SerializeField] private ConnectToServer _connectToServer;
        [SerializeField] private RoomsManager _roomsManager;
        
        private Dictionary<int, Vector3> selectorDictionary = new Dictionary<int, Vector3>();
        private int _indexer;
        
        private void Start()
        {
            selectorDictionary.Add(0, _pointer.anchoredPosition);
            selectorDictionary.Add(1, Vector3.zero);
            selectorDictionary.Add(2, new Vector3(0, -205, 0));
        }

        private void Update()
        {
            CheckForArrowUpdate();
        }

        private void CheckForArrowUpdate()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                _indexer = _indexer == 2 ? 0 : _indexer + 1;
                _pointer.DOAnchorPos(selectorDictionary[_indexer], 1).SetEase(Ease.OutSine);
            }else if (Input.GetKeyDown(KeyCode.W))
            {
                _indexer = _indexer == 0 ? 2 : _indexer - 1;
                _pointer.DOAnchorPos(selectorDictionary[_indexer], 1).SetEase(Ease.OutSine);
            }
            else if(Input.GetKeyDown(KeyCode.Return)){
                switch (_indexer)
                {
                    case 0: LoadLobby();
                        break;
                    case 1: DisplayOptions();
                        break;
                    case 2: Application.Quit();
                        break;
                    default: Debug.LogError("Index out of scope");
                        break;
                }
            }
        }

        public void ChangeScene(int index) => SceneManager.LoadScene(index);

        public void LoadLobby()
        {
            _lobbyScreen.SetActive(true);
            _menuScreen.SetActive(false);
            _connectToServer.JoinLobby();
        }

        public void DisplayMenu()
        {
            _loadingScreen.SetActive(false);
            _menuScreen.SetActive(true);
            _lobbyScreen.SetActive(false);
            _roomScreen.SetActive(false);
            
            if(!PhotonNetwork.InLobby) return;
            _connectToServer.LeaveLobby();
            
            if (!PhotonNetwork.InRoom) return;
            
            PhotonNetwork.LeaveRoom();
        }

        public void DisplayRoom()
        {
            _lobbyScreen.SetActive(false);
            _roomScreen.SetActive(true);
            _roomName.text = "Room Name: " + RoomsManager.GetRoomName();

            if (!PhotonNetwork.IsMasterClient)
                _playButton.interactable = false;
        }

        public void DisplayOptions()
        {
            _optionsScreen.SetActive(!_optionsScreen.activeSelf);
            _menuScreen.SetActive(!_menuScreen.activeSelf);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
