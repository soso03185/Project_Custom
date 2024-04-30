using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    public Dictionary<string, ObjectPool> pools = new Dictionary<string, ObjectPool>();


    public void CreatePool(string path, int size)
    {
        ObjectPool pool = new ObjectPool();
        pool.CreateObject(path, size);

        pools.Add(path, pool);
    }

    public void Init()
    {
        CreatePool("Fox", 100);
        CreatePool("Skeleton", 100);
    }

    public ObjectPool GetPool(string path)
    {
        foreach(var pool in pools)
        {
            if (pool.Key == path)
            {
                return pool.Value;
            }
        }

        return null;
    }
}
