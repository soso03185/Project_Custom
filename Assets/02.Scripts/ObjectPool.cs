using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ObjectPool
{
    // ΩÃ±€≈Ê
    //public static ObjectPool Instance;

    private GameObject poolingObjectPrefab;

    public List<GameObject> objectsList = new List<GameObject>();

    public int poolSize = 200;

    public Transform parentTransform;

    public int activeCount;

    public void CreateObject(string path, int size)
    {
        for (int i = 0; i < size; i++)
        {
            //GameObject obj = Instantiate(poolingObjectPrefab, parentTransform);
            poolingObjectPrefab = Managers.Resoruce.Instantiate(path, parentTransform);

            poolingObjectPrefab.SetActive(false);
            objectsList.Add(poolingObjectPrefab);
        }
    }

    // ø¿πˆ∑Œµ˘
    public GameObject GetGameObject(string path, Vector3 trans)
    {
        GameObject select = null;
        foreach(GameObject obj in objectsList) 
        {
            if(!obj.activeSelf)
            {
                select = obj;
                if(select.name == path + "(Clone)")
                {
                    select.transform.position = trans;
                    select.SetActive(true);
                    activeCount++;
                    break;
                }
            }

        }

        if (!select)
        {
            select = Managers.Resoruce.Instantiate(path, parentTransform); ;
            select.transform.position = trans;
            objectsList.Add(select);
            activeCount++;
        }

        return select;
    }

    public GameObject GetGameObject(string path)
    {
        GameObject select = null;

        foreach (GameObject obj in objectsList)
        {
            if (!obj.activeSelf)
            {
                select = obj;
                select.SetActive(true);
                activeCount++;
                break;
            }
        }

        if (!select)
        {
            select = Managers.Resoruce.Instantiate(path, parentTransform); ;
            objectsList.Add(select);
            activeCount++;
        }

        return select;
    }

    // return«‘ºˆ
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        activeCount--;
    }
}
