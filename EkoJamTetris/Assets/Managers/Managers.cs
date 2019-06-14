using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Managers : MonoBehaviour
{
    public static Managers Manager { get; private set; }

    void Awake()
    {
        if (Manager == null)
        {
            Manager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
