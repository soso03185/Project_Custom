using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers s_instance;
    public static Managers Instance
    {
        get
        {
            Init();
            return s_instance;
        }
    }

    public static ResourceManager Resoruce { get { return Instance.resource; } }

    public static PoolManager Pool { get { return Instance.pool; } }

    //public static MonsterManager Monsters { get { return Instance.monsters; } }


    PoolManager pool = new PoolManager();
    ResourceManager resource = new ResourceManager();
    //MonsterManager monsters = new MonsterManager();

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance.pool.Init();
        }
    }

}