using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FungiUtilities
{
    public static Vector3 ChangeDirection(Vector3 currentDirection, ChangeTypes changeType)
    {
        Vector3 newDirection = currentDirection;
        
        switch (changeType)
        {
            case ChangeTypes.CROSS:
                newDirection = Vector3.Cross(currentDirection, Vector3.up);
                break;
            case ChangeTypes.RANDOM:
                newDirection = new Vector3(Random.Range(0, 100), 0 ,Random.Range(0, 1));
                break;
            case ChangeTypes.ANGLE:
                float randomAngle = Random.Range(-180, 180);
                randomAngle = randomAngle * (Mathf.PI / 180);
                newDirection.x = currentDirection.magnitude * Mathf.Cos(randomAngle);
                newDirection.z = currentDirection.magnitude * Mathf.Sin(randomAngle);
                break;
        }

        return newDirection;
    }
    
    public enum ChangeTypes
    {
        CROSS,
        RANDOM,
        ANGLE
    }
    
    public enum FungiAttacks
    {
        CrossShot,
        SporeCloud,
        AcidRain,
        Dash,
        Minions,
        Ultimate
    }
}
