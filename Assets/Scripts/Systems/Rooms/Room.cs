using Units;
using UnityEngine;

public class Room : MonoBehaviour
{
    public BaseUnit[] EnemiesInRoom;

    public void SpawnEnemies()
    {
        for (int i = 0; i < EnemiesInRoom.Length; i++)
        {
            EnemiesInRoom[i].Spawn();
        }
    }

    public void DespawnEnemies()
    {
        for (int i = 0; i < EnemiesInRoom.Length; i++)
        {
            EnemiesInRoom[i].Kill();
        }
    }
}