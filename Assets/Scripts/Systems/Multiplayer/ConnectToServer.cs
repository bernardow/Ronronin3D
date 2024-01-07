using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using Photon.Pun;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private MenuManager _menuManager;
    
    private void Start() => PhotonNetwork.ConnectUsingSettings();

    public void JoinLobby() => PhotonNetwork.JoinLobby();

    public void LeaveLobby() => PhotonNetwork.LeaveLobby();

    public override void OnConnectedToMaster()
    {
        _menuManager.DisplayMenu();
        Debug.Log("Connected");
    }
}
