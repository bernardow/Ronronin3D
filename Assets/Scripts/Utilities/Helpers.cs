using System;
using System.Collections;
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

        public static Vector3 CheckForOutScreen(Vector3 currentDestination, float maxX, float minX, float maxZ, float minZ)
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

        public static Vector3 CheckForInSetupCollision(Vector3 currentDirection, Transform currentTransform, Collider collider, LayerMask mask)
        {
            Vector3 newDirection = currentDirection;
            Vector3 currentPosition = currentTransform.localPosition;
            Ray ray = new Ray(currentPosition, currentDirection);
            //Debug.DrawRay(currentPosition, currentDirection * 1);
            if (Physics.Raycast(ray, out RaycastHit hit, 5.5f, mask))
            {
                if (hit.collider.CompareTag("Setup"))
                {
                    Bounds playerBounds = collider.bounds;
                    Vector3 size = playerBounds.size;
                    newDirection = newDirection.normalized * Vector3.Distance(currentPosition, hit.point);// - new Vector3(size.x, 0, size.z);
                    //Debug.DrawLine(currentPosition, newDirection, Color.magenta);
                    return newDirection;
                }
            }

            return newDirection;
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

        public static int GetRandomValueInList<T>(this List<T> currentList, int initialNumber = 0, int maxNumber = 0)
        {
            if (initialNumber != 0)
                return Random.Range(initialNumber, maxNumber);
            
            if (maxNumber != 0)
                return Random.Range(0, maxNumber);
            return Random.Range(0, currentList.Count);
        }

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
            Vector3 o = Vector3.forward;
            o = boss != null ? boss!.position : o;
            return o;
        }
        
        public static Vector3 SearchForWalls(Vector3 startPoint, Vector3 direction, LayerMask mask,float range = 2.5f)
        {
            Vector3 leftPoint = Vector3.Cross(direction, Vector3.up);
            Vector3 rightPoint = -leftPoint;
            
            Ray ray = new Ray(rightPoint + startPoint, direction * range);
            if (Physics.Raycast(ray, out RaycastHit hit, range, mask))
            {
                if (hit.collider.CompareTag("Setup"))
                {
                    direction = -direction;
                    return direction;
                }
            }
            
            Ray leftRay =  new Ray(leftPoint + startPoint, direction * range);
            if (Physics.Raycast(leftRay, out RaycastHit hitInfo, range, mask))
            {
                if (hitInfo.collider.CompareTag("Setup"))
                {
                    direction = -direction;
                    return direction;
                }
            }

            return direction;
        }

        public enum Bosses
        {
            Fungi
        }
    }
}
