using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utilities
{
    public static class Helpers
    {
        private static Vector2 ClampedAngle(float angle)
        {
            switch (angle)
            {
                case (> 22.5f and < 67.5f) or (< -292.5f and > -337.5f): 
                    return new Vector2(0.5f, 0.5f);
                    
                case (> 67.5f and < 112.5f) or (< -247.5f and > -292.5f):
                    return new Vector2(0, 1);
                    
                case (> 112.5f and < 157.5f) or (< -202.5f and > -247.5f):
                    return new Vector2(-0.5f, 0.5f);

                case (> 157.5f and < 202.5f) or (< -157.5f and > -202.5f):
                    return new Vector2(-1, 0);
                    
                case (> 202.5f and < 247.5f) or (< -112.5f and > -157.5f):
                    return new Vector2(-0.5f, -0.5f);
                    
                case (> 247.5f and < 292.5f) or (< -67.5f and > -112.5f):
                    return new Vector2(0, -1);
                    
                case (> 292.5f and < 337.5f) or (< -22.5f and > -67.5f):
                    return new Vector2(0.5f, -0.5f);
                    
                default:
                    return new Vector2(1, 0);
            }
        }

        public static Vector3 CheckForOutScreen(float maxX, float minX, float maxZ, float minZ, Vector3  currentDestination)
        {
            Vector3 newDestination =  currentDestination;

            if (currentDestination.x > maxX)
                newDestination.x = maxX;
            else if (currentDestination.x < minX)
                newDestination.x = minX;

            if (currentDestination.z > maxZ)
                newDestination.z = maxZ;
            else if (currentDestination.z < minZ)
                newDestination.z = minZ;

            return newDestination;
        }

        private static Vector2 ClampedAngleLockedDirections(float angle)
        {
            switch (angle)
            {
                case > -45f and < 45f:
                    return Vector2.right;
                    
                case > 45f and < 135f:
                    return Vector2.up;
                    
                case (> 135f and < 225f) or (< -135f and > -225f):
                    return Vector2.right * -1;

                case (> 225 and < 315) or (< -45f and > -135f):
                    return Vector2.up * -1;
                    
                default:
                    return new Vector2(1, 0);
            }
        }

        public static int GetRandomValueInList<T>(this List<T> currentList) => Random.Range(0, currentList.Count);

        public static T GetChildComponent<T>(this GameObject caller)
        {
            T[] allComponents = caller.GetComponentsInChildren<T>();
            
            if (caller.GetComponent<T>() != null && allComponents.Length > 1)
                return allComponents[1];

            return allComponents[0];
        }

        public static Vector3 GetPlayerPosition()
        {
            Transform player = GameObject.FindWithTag("Player").transform;
            return player.position;
        }
        
        public static Vector3 GetBossPosition()
        {
            Transform boss = GameObject.FindWithTag("Boss")!.transform!;
            Vector3 o = Vector3.zero;
            o = boss != null ? boss.position : o;
            return o;
        }
    }
}
