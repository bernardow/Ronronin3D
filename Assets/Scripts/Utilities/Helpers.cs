using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utilities
{
    public static class Helpers
    {
        private const float Vector2RightMagnitude = 1;
        
        public static Vector3 GetDirection(bool mobile = false)
        {
            /*
            if (mobile)
            {
                Vector2 dir = outPos - inputPos;
                float dotProduct = Vector2.Dot(dir, Vector2.right);
                float combinedMagnitudes = Vector3.Magnitude(dir) * Vector2RightMagnitude;

                dotProduct /= combinedMagnitudes;
                dotProduct = Mathf.Acos(dotProduct);
                dotProduct *= (180 / Mathf.PI);
                dotProduct = dir.y < 0? dotProduct * -1: dotProduct;
                dir = lockedDirections? ClampedAngleLockedDirections(dotProduct) : ClampedAngle(dotProduct);
                return dir;
            }*/
            
            float horVal = Input.GetAxisRaw("Horizontal");
            float verVal = Input.GetAxisRaw("Vertical");
            
            return new Vector3(horVal, 0, verVal);
        }
        
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
    }
}
