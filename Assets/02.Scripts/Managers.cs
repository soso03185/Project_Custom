using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers Instance { get; private set; }

    public static ResourceManager Resoruce { get { return Instance.resource; } }

    public static PoolManager Pool { get { return Instance.pool; } }

    PoolManager pool;
    ResourceManager resource;
    private void Awake()
    {
        Instance = this;
        pool = new PoolManager();
        resource = new ResourceManager();
        pool.Init();
    }
}
