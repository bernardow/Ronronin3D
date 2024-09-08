using System;
using System.Collections;
using System.Collections.Generic;
using Systems;
using UnityEngine;

public class RoomsController : Controller<RoomsController>
{
    public Room[] Rooms;
    public Room CurrentRoom;

    public static event Action<Room> OnPlayerEnterRoom;
    public static event Action<Room> OnPlayerLeftRoom;

    public void ChangeRoom(Room newRoom)
    {
        OnPlayerLeftRoom?.Invoke(CurrentRoom);

        CurrentRoom = newRoom;
        
        OnPlayerEnterRoom?.Invoke(CurrentRoom);
    }
}