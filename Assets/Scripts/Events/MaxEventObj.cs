using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "MaxEvent", menuName = "Max Event System/Create New", order = 0)]
[System.Serializable] public class MaxEventObj : ScriptableObject
{
    [SerializeField] public UnityEvent mevent = new UnityEvent();    
    [SerializeField] public UnityAction maction;    
    public void Register(UnityAction listener)
    {
        mevent.AddListener(listener);
    }
      
    public void Unregister(UnityAction listener)
    {
        mevent.AddListener(listener);
    }

    public void Raise()
    {
        mevent.Invoke();
    }

}
