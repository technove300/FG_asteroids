using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]public class MaxEvent
{
    [SerializeField]public List<MaxEventObj> callbacks = new List<MaxEventObj>();

    public void Invoke()
    {
        foreach (MaxEventObj e in callbacks)
        {
            e.Raise();
        }
    }
}
