using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBehavior<T> : MonoBehaviour where T:SingletonBehavior<T>
{
  public static T Instance 
    {
        get;
        private set;
    }

    public virtual void Awake() 
    {
        if (Instance == null)
        {
            Instance = this as T;
        }
    }
}
