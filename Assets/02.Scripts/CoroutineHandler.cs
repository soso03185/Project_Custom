using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHandler : MonoBehaviour
{
    public static CoroutineHandler instance;

    private void Awake()
    {
        instance = this;
    }

    public Coroutine StartMyCoroutine(IEnumerator routine)
    {
        return StartCoroutine(routine);
    }

    public void StopMyCoroutine(IEnumerator routine)
    {
        if(routine != null)
        {
            StopCoroutine(routine);
        }
    }

    public void StopAllMyCoroutines()
    {
        UnityEngine.Coroutine[] allCoroutines = GetComponentsInChildren<UnityEngine.Coroutine>();
        foreach(var coroutine in allCoroutines)
        {
            StopCoroutine(coroutine);
        }
    }
}
