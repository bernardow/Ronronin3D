using System;
using System.Collections;
using System.Linq;
using Units;
using Units.Player;
using UnityEngine;

public class Room : MonoBehaviour
{
    public BaseUnit[] EnemiesInRoom;
    public Entry[] Entries;

#if UNITY_EDITOR
    public Color gizmosColor = Color.blue;
#endif

    private void Awake()
    {
        RoomsController.OnPlayerEnterRoom += SpawnEnemies;
        RoomsController.OnPlayerLeftRoom += DespawnEnemies;
    }

    private void OnDisable()
    {
        RoomsController.OnPlayerEnterRoom -= SpawnEnemies;
        RoomsController.OnPlayerLeftRoom -= DespawnEnemies;
    }

    public void SpawnEnemies(Room room)
    {
        if (room != this) return;
        
        for (int i = 0; i < EnemiesInRoom.Length; i++)
        {
            EnemiesInRoom[i].Spawn();
        }
    }

    public void DespawnEnemies(Room room)
    {
        if (room != this) return;
        
        for (int i = 0; i < EnemiesInRoom.Length; i++)
        {
            EnemiesInRoom[i].Kill();
        }
    }

    private Entry GetPlayerNearestEntry()
    {
        float lesserDistance = 9999;
        Entry nearestEntry = Entries[0];
        
        for (int i = 0; i < Entries.Length; i++)
        {
            if (Entries[i].DistanceToPlayer < lesserDistance)
            {
                lesserDistance = Entries[i].DistanceToPlayer;
                nearestEntry = Entries[i];
            }
        }

        return nearestEntry;
    }
    

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (RoomsController.Instance == null)
        {
            Gizmos.color = gizmosColor;
        } else Gizmos.color = RoomsController.Instance.CurrentRoom == this ? Color.green : Color.blue;
        
        Gizmos.DrawCube(transform.position, Vector3.one * 10);
    }
#endif
}