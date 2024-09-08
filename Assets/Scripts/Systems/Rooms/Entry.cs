using System;
using System.Collections;
using System.Collections.Generic;
using Units.Player;
using UnityEngine;

public class Entry : MonoBehaviour
{
    public Room InnerRoom;
    public Room OutterRoom;

    private Room parentRoom;

    private Vector3 playerOnEnterPosition;
    private Vector3 playerOnExitPosition;
    
    public float DistanceToPlayer => Vector3.Distance(transform.position, Player.Instance.PlayerTransform.position);

    private void Awake()
    {
        parentRoom = GetComponentInParent<Room>();
    }
    
    public bool IsPlayerEnteringRoom()
    {
        Vector3 parentRoomPosition = parentRoom.transform.position;
        float distanceLastFrame = Vector3.Distance(parentRoomPosition, playerOnEnterPosition);
        float distance = Vector3.Distance(parentRoomPosition, playerOnExitPosition);
        return distance < distanceLastFrame;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerOnEnterPosition = Player.Instance.PlayerTransform.position;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        playerOnExitPosition = Player.Instance.PlayerTransform.position;
        RoomsController.Instance.ChangeRoom(IsPlayerEnteringRoom() ? InnerRoom : OutterRoom);    }
}
