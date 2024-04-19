using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // ΩÃ±€≈Ê
    public static ObjectPool Instance;

    [SerializeField]
    private GameObject poolingObjectPrefab;

    public List<GameObject> objectsList;

    public int poolSize = 200;

    private void Awake()
    {
        Instance = this;
        objectsList = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(poolingObjectPrefab);

            obj.SetActive(false);
            objectsList.Add(obj);
        }
    }

    // ø¿πˆ∑Œµ˘
    public GameObject GetGameObject(Vector3 trans)
    {
        GameObject select = null;
        
        foreach(GameObject obj in objectsList) 
        {
            if(!obj.activeSelf)
            {
                select = obj;
                select.transform.position = trans;
                select.SetActive(true);
                break;
            }

        }

        if (!select)
        {
            select = Instantiate(poolingObjectPrefab);
            select.transform.position = trans;
            objectsList.Add(select);
        }

        return select;
    }

    public GameObject GetGameObject()
    {
        GameObject select = null;

        foreach (GameObject obj in objectsList)
        {
            if (!obj.activeSelf)
            {
                select = obj;
                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(poolingObjectPrefab);
            objectsList.Add(select);
        }

        return select;
    }

    // return«‘ºˆ 
}
