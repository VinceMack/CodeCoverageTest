using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PriorityQueue
{
    private List<KeyValuePair<BaseTile, int>> elements = new List<KeyValuePair<BaseTile, int>>();

    public int Count
    {
        get { return elements.Count; }
    }

    public void Enqueue(BaseTile item, int priority)
    {
        elements.Add(new KeyValuePair<BaseTile, int>(item, priority));
    }

    public BaseTile Dequeue()
    {
        int bestIndex = 0;

        for (int i = 1; i < elements.Count; i++)
        {
            if (elements[i].Value < elements[bestIndex].Value)
            {
                bestIndex = i;
            }
        }

        BaseTile bestItem = elements[bestIndex].Key;
        elements.RemoveAt(bestIndex);
        return bestItem;
    }

    public bool Contains(BaseTile item)
    {
        return elements.Exists(element => element.Key.Equals(item));
    }

    public void Clear()
    {
        elements.Clear();
    }
}