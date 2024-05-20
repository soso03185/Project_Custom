using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UGS;
using static DataManager;
using static Define;
using System;

public class DataManager
{
    public struct UserInfo
    {
        public int UserID;
        public string Name;
        public int Level;
        public float Hp;
        public float Atk;
        public float Defense;
        public float MoveSpeed;
        public float AttackSpeed;
        public float AttackRange;
        public float LogicChangeSpeed;
        public int NowExp;
        public float CriticalChance;
        public float CriticalMultiplier;
        public int Equipment;
        public int Gold;
        public int Goods;
        public int Goods2;
        public int Goods3;
        public int Costume;
        public int StageLevel;
    }

    [System.Serializable]
    public struct MonsterInfo
    {
        public int MonsterID;
        public string Name;
        public int Level;
        public float Hp;
        public float Atk;
        public float Defense;
        public float MoveSpeed;
        public float AttackSpeed;
        public float AttackRange;
        public int NowExp;
        public int Gold;
        public int Goods;
        public int Goods2;
    }

    public struct StageInfo
    {
        public int key;
        public int stageLevel;
        public int maxMonsterCount;
        public MonsterName monster1Name;
        public int monster1Count;
        public SpawnType spawnType1;
        public MonsterName monster2Name;
        public int monster2Count;
        public SpawnType spawnType2;
        public bool isBossStage;
        public string bossMonsterName;
        public int monsterTypeCount;
    }

    public UGS_DataManager UGS_Data;

    public UserInfo GetUserInfo(int UserID)
    {
        if(UGS_Data == null)
        {
            UGS_Data = GameObject.Find("DataLoadTest").GetComponent<UGS_DataManager>();
        }

        UserInfo userInfo;

        userInfo.UserID = UGS_Data.m_UserDataDic[UserID].UserID;
        userInfo.Name = UGS_Data.m_UserDataDic[UserID].Name;
        userInfo.Level = UGS_Data.m_UserDataDic[UserID].Level;
        userInfo.Hp = UGS_Data.m_UserDataDic[UserID].Hp;
        userInfo.Atk = UGS_Data.m_UserDataDic[UserID].Atk;
        userInfo.Defense = UGS_Data.m_UserDataDic[UserID].Defense;
        userInfo.MoveSpeed = UGS_Data.m_UserDataDic[UserID].MoveSpeed;
        userInfo.AttackSpeed = UGS_Data.m_UserDataDic[UserID].AttackSpeed;
        userInfo.AttackRange = UGS_Data.m_UserDataDic[UserID].AttackRange;
        userInfo.LogicChangeSpeed = UGS_Data.m_UserDataDic[UserID].LogicChangeSpeed;
        userInfo.NowExp = UGS_Data.m_UserDataDic[UserID].NowExp;
        userInfo.CriticalChance = UGS_Data.m_UserDataDic[UserID].CriticalChance;
        userInfo.CriticalMultiplier = UGS_Data.m_UserDataDic[UserID].CriticalMultiplier;
        userInfo.Equipment = UGS_Data.m_UserDataDic[UserID].Equipment;
        userInfo.Gold = UGS_Data.m_UserDataDic[UserID].Gold;
        userInfo.Goods = UGS_Data.m_UserDataDic[UserID].Goods;
        userInfo.Goods2 = UGS_Data.m_UserDataDic[UserID].Goods2;
        userInfo.Goods3 = UGS_Data.m_UserDataDic[UserID].Goods3;
        userInfo.Costume = UGS_Data.m_UserDataDic[UserID].Costume;
        userInfo.StageLevel = UGS_Data.m_UserDataDic[UserID].StageLevel;

        return userInfo;
    }

    public MonsterInfo GetMonsterInfo(int monsterID)
    {
        if (UGS_Data == null)
        {
            UGS_Data = GameObject.Find("DataLoadTest").GetComponent<UGS_DataManager>();
        }

        MonsterInfo monsterInfo;
        
        monsterInfo.MonsterID = UGS_Data.m_MonsterDataDic[monsterID].MonsterID;
        monsterInfo.Name = UGS_Data.m_MonsterDataDic[monsterID].Name;
        monsterInfo.Level = UGS_Data.m_MonsterDataDic[monsterID].Level;
        monsterInfo.Hp = UGS_Data.m_MonsterDataDic[monsterID].Hp;
        monsterInfo.Atk = UGS_Data.m_MonsterDataDic[monsterID].Atk;
        monsterInfo.Defense = UGS_Data.m_MonsterDataDic[monsterID].Defense;
        monsterInfo.MoveSpeed = UGS_Data.m_MonsterDataDic[monsterID].MoveSpeed;
        monsterInfo.AttackSpeed = UGS_Data.m_MonsterDataDic[monsterID].AttackSpeed;
        monsterInfo.AttackRange = UGS_Data.m_MonsterDataDic[monsterID].AttackRange;
        monsterInfo.NowExp = UGS_Data.m_MonsterDataDic[monsterID].NowExp;
        monsterInfo.Gold = UGS_Data.m_MonsterDataDic[monsterID].Gold;
        monsterInfo.Goods = UGS_Data.m_MonsterDataDic[monsterID].Goods;
        monsterInfo.Goods2 = UGS_Data.m_MonsterDataDic[monsterID].Goods2;

        return monsterInfo;
    }

    public StageInfo GetStageInfo(int stageLevel)
    {
        if (UGS_Data == null)
        {
            UGS_Data = GameObject.Find("DataLoadTest").GetComponent<UGS_DataManager>();
        }

        StageInfo stageInfo;

        stageInfo.key = UGS_Data.m_StageDataDic[stageLevel].Key;
        stageInfo.stageLevel = UGS_Data.m_StageDataDic[stageLevel].StageLevel;
        stageInfo.maxMonsterCount = UGS_Data.m_StageDataDic[stageLevel].MaxMonsterCount;
        stageInfo.monster1Name = (MonsterName)Enum.Parse(typeof(MonsterName), UGS_Data.m_StageDataDic[stageLevel].Monster1Name);
        stageInfo.monster1Count = UGS_Data.m_StageDataDic[stageLevel].Monster1Count;
        stageInfo.spawnType1 = (SpawnType)Enum.Parse(typeof(SpawnType), UGS_Data.m_StageDataDic[stageLevel].SpawnOption1);
        stageInfo.monster2Name = (MonsterName)Enum.Parse(typeof(MonsterName), UGS_Data.m_StageDataDic[stageLevel].Monster2Name);
        stageInfo.monster2Count = UGS_Data.m_StageDataDic[stageLevel].Monster2Count;
        stageInfo.spawnType2 = (SpawnType)Enum.Parse(typeof(SpawnType), UGS_Data.m_StageDataDic[stageLevel].SpawnOption2);
        stageInfo.isBossStage = System.Convert.ToBoolean(UGS_Data.m_StageDataDic[stageLevel].IsBossStage);
        stageInfo.bossMonsterName = UGS_Data.m_StageDataDic[stageLevel].BossMonster;
        stageInfo.monsterTypeCount = UGS_Data.m_StageDataDic[stageLevel].MonsterTypeCount;

        if (UGS_Data.m_StageDataDic[stageLevel] == null)
        {
            Application.Quit();
        }

        return stageInfo;
    }
}
