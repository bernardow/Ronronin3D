using System.Collections;
using System.Collections.Generic;
using Managers;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class RoomsManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private MenuManager _menuManager;
    
    public void CreateRoom()
    {
        string roomName = Helpers.GetHashCode();
        PhotonNetwork.CreateRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        _menuManager.DisplayRoom();
    }

    public void JoinRoom() => PhotonNetwork.JoinRoom(_inputField.text);

    public void Play() => photonView.RPC("LoadGameScene", RpcTarget.All);

    [PunRPC]
    public void LoadGameScene()
    {
        PhotonNetwork.LoadLevel("FungiPalace");
    }
    
    public static string GetRoomName() => PhotonNetwork.CurrentRoom.Name;
    
}
