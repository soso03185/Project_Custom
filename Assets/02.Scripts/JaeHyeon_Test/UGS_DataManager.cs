using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UGS;
using GoogleSheet.Type;
using System;

public class UGS_DataManager : MonoBehaviour
{
    Dictionary<int, ProjectCustom_UserData.UserData> m_UserDataDic = new Dictionary<int, ProjectCustom_UserData.UserData>();
    Dictionary<int, ProjectCustom_UserData.StandardData> m_StandardDataDic = new Dictionary<int, ProjectCustom_UserData.StandardData>();
    Dictionary<int, ProjectCustom_MonsterData.MonsterData> m_MonsterDataDic = new Dictionary<int, ProjectCustom_MonsterData.MonsterData>();

    public enum ShowDataType
    {
        User = 0,
        Standard = 1,
        Monster = 2
    }

    void Awake()
    {
        UnityGoogleSheet.LoadAllData();
    }

    private void Start()
    {
        UserDataLocalLoad();
        StandardDataLocalLoad();
        MonsterDataLocalLoad();
    }

    //------------------------------------ StandardData ------------------------------------
    public void StandardDataLocalLoad()
    {
        Debug.Log("StandardData_LocalLoad");

        m_StandardDataDic.Clear();

        foreach (var value in ProjectCustom_UserData.StandardData.StandardDataList)
        {
            m_StandardDataDic.Add(value.Level, value);
        }
    }
    public void StandardDataLiveLoad()
    {
        Debug.Log("StandardData_LiveLoad");

        m_StandardDataDic.Clear();

        UnityGoogleSheet.LoadFromGoogle<int, ProjectCustom_UserData.StandardData>((list, map) => {
            list.ForEach(x => {
                m_StandardDataDic.Add(x.Level, x);
            });
        }, true);
    }

    //------------------------------------ UserData ------------------------------------
    public void UserDataLocalLoad()
    {
        Debug.Log("UserData_LocalLoad");

        m_UserDataDic.Clear();

        foreach (var value in ProjectCustom_UserData.UserData.UserDataList)
        {
            m_UserDataDic.Add(value.UserID, value);
        }
    }
    public void UserDataLiveLoad()
    {
        Debug.Log("UserData_LiveLoad");

        m_UserDataDic.Clear();

        UnityGoogleSheet.LoadFromGoogle<int, ProjectCustom_UserData.UserData>((list, map) => {
            list.ForEach(x => {
                m_UserDataDic.Add(x.UserID, x);
            });
        }, true);
    }

    //------------------------------------ Monster ------------------------------------
    public void MonsterDataLocalLoad()
    {
        Debug.Log("MonsterData_LocalLoad");

        m_MonsterDataDic.Clear();

        foreach (var value in ProjectCustom_MonsterData.MonsterData.MonsterDataList)
        {
            m_MonsterDataDic.Add(value.MonsterID, value);
        }
    }
    public void MonsterDataLiveLoad()
    {
        Debug.Log("MonsterData_LiveLoad");

        m_MonsterDataDic.Clear();

        UnityGoogleSheet.LoadFromGoogle<int, ProjectCustom_MonsterData.MonsterData>((list, map) => {
            list.ForEach(x => {
                m_MonsterDataDic.Add(x.MonsterID, x);
            });
        }, true);
    }

    //------------------------------------ Show ------------------------------------
    [VisibleEnum(typeof(ShowDataType))]
    public void ShowData(int _showDataType)
    {
        switch(_showDataType)
        {
            case (int)ShowDataType.User:
                {
                    foreach (var child in m_UserDataDic)
                        Debug.Log($"PlayerName: {child.Value.Name}, UserID: {child.Value.UserID}");
                }
                break;
            case (int)ShowDataType.Standard:
                {
                    foreach (var child in m_StandardDataDic)
                        Debug.Log($"Level: {child.Value.Level}, NeedExp: {child.Value.NeedExp}");
                }
                break;
            case (int)ShowDataType.Monster:
                {
                    foreach (var child in m_MonsterDataDic)
                        Debug.Log($"MonsterName: {child.Value.Name}, MonsterID: {child.Value.MonsterID}");
                }
                break;
        }
    }
}
