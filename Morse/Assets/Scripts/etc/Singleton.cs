using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : class
{
    public static T instance;

    protected void Awake()
    {
        instance = (this as T);
    }
}
