using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue<T>
{
    public int Length { get; private set; }
    private List<T> _queue = new List<T>();
    
    public void AddToQueue(T item)
    {
        _queue.Add(item);
        Length++;
    }

    public void RemoveFromQueue(T item)
    {
        if (_queue.Contains(item))
        {
            _queue.Remove(item);
            Length--;
        }
    }

    public T GetNextInQueue()
    {
        T nextInQueue = _queue[0];
        _queue.Remove(_queue[0]);
        Length--;
        return nextInQueue;
    }

    public void ClearQueue()
    {
        _queue.Clear();
        Length = 0;
    }
}
