using System;
using System.Collections;
using Photon.Pun;
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
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 1;
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(3f);
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.Instantiate(_bossPrefab.name, new Vector3(0, 0.86f, 7.1f), Quaternion.identity);
            PhotonNetwork.Instantiate(_playerPrefab.name, Vector3.zero, quaternion.identity);
            
        }
    }
}
