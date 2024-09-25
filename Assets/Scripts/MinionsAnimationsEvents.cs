using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MinionsAnimationsEvents : MonoBehaviour
{
    [SerializeField] private UnityEvent[] AnimationEvents;

    public void ShootAnimationEvent(int index)
    {
        AnimationEvents[index].Invoke();
    }
}
