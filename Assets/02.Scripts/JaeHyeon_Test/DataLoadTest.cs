using System.Collections;
using System.Collections.Generic;
using UGS;
using UnityEngine;

public class DataLoadTest : MonoBehaviour
{
    void Awake()
    {
        UnityGoogleSheet.LoadAllData();
        // UnityGoogleSheet.Load<DefaultTable.Data.Load>(); it's same!
        // or call DefaultTable.Data.Load(); it's same!
    }

    void Start()
    {
        
    }

    public void LocalLoad()
    {
        Debug.Log("LocalLoad");

        foreach (var value in ProjectCustom_UserData.UserData.UserDataList)
        {
            Debug.Log(value.Name + "," + value.Level + "," + value.Hp);
        }
        var dataFromMap = ProjectCustom_UserData.UserData.UserDataMap[0];
        Debug.Log("dataFromMap : " + dataFromMap.Name + ", " + dataFromMap.Level + "," + dataFromMap.Hp);
    }

    public void LiveLoad()
    {
        Debug.Log("LiveLoad");

        UnityGoogleSheet.LoadFromGoogle<int, ProjectCustom_UserData.UserData> ((list, map) => {
            list.ForEach(x => {
                Debug.Log(x.Hp);
            });
        }, true);
    }

    public void LiveWrite()
    {
        Debug.Log("LiveWrite");

        var newData = new ProjectCustom_UserData.UserData();
        newData.Name = "MyPlayer";
        newData.Level = 300;
        newData.Hp = 33;

        UnityGoogleSheet.Write<ProjectCustom_UserData.UserData>(newData);
    }
}
