using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public class StageManager
{
    // ���� ��������

    // �������� ����
    //public int stageLevel = 1;
    // �������� ���� UI
    public TextMeshProUGUI stageLevelUI;
    // �� ���������� ���� ��
    public int maxMonsterCount = 40;

    // ���� ���� ��
    public int deadMonsterCount;

    // ���� ����

    // ���� �������������� ����
    public bool isBossStage;
    // ���� ���� ������
    //public GameObject boss;
    // ���� �������� ���� ���� �ð�
    public float timeLimit;

    // ���� �ð� UI

    // ��� ���ҽ� ������

    public DataManager.StageInfo stageInfo;

    public List<SpawnOptions> spawnList = new List<SpawnOptions>();

    [System.Serializable]
    public struct SpawnOptions
    {
        public SpawnType spawnType;
        public MonsterName monsterName;
        public int monsterCount;
    }

    SpawnOptions monster1Spawn;
    SpawnOptions monster2Spawn;
    SpawnOptions bossSpawn;

    public void BeginStage(int stageLevel)
    {
        GetStageData(stageLevel);
        monster1Spawn.spawnType = stageInfo.spawnType1;
        monster1Spawn.monsterName = stageInfo.monster1Name;
        monster1Spawn.monsterCount = stageInfo.monster1Count;

        monster2Spawn.spawnType = stageInfo.spawnType2;
        monster2Spawn.monsterName = stageInfo.monster2Name;
        monster2Spawn.monsterCount = stageInfo.monster2Count;

        spawnList.Add(monster1Spawn);
        spawnList.Add(monster2Spawn);

        if(stageInfo.isBossStage)
        {
            SpawnController.spawnInstance.StartSpawning(stageInfo.maxMonsterCount, 1, stageInfo.bossMonsterName, SpawnType.Boss);
        }

        foreach(var temp in spawnList)
        {
            SpawnController.spawnInstance.StartSpawning(stageInfo.maxMonsterCount, temp.monsterCount, temp.monsterName.ToString(), temp.spawnType);
        }
    }

    public void LevelUpStage(int stageLevel)
    {
        deadMonsterCount = 0;
        stageLevel++;

        Debug.Log($"Level up stage : {stageLevel}");

        BeginStage(stageLevel);
    }

    public void GetStageData(int stageLevel)
    {
        stageInfo = Managers.Data.GetStageInfo(stageLevel);
    }

    public bool isStageClear()
    {
        if (deadMonsterCount == stageInfo.maxMonsterCount)
        {
            return true;
        }
        else
            return false;
    }

    public void RestartStage(int stageLevel)
    {
        BeginStage(stageLevel);
    }
}
