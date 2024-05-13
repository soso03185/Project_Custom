using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StageManager
{
    // ���� ��������

    // �������� ����
    public int stageLevel = 1;
    // �������� ���� UI
    public TextMeshProUGUI stageLevelUI;
    // �� ���������� ���� ��
    public int maxMonsterCount;

    // ���� ����

    // ���� �������������� ����
    public bool isBossStage;
    // ���� ���� ������
    //public GameObject boss;
    // ���� �������� ���� ���� �ð�
    public float timeLimit;
    // ���� �ð� UI

    // Ŭ���� ���� ����
    public bool isStageClear;

    // ��� ���ҽ� ������

    public DataManager.StageInfo stageInfo;

    public void LevelUpStage()
    {
        stageLevel++;
        //stageLevelUI.GetComponent<TextMeshProUGUI>().text = stageLevel.ToString();

        GetStageData(stageLevel);
    }

    public void GetStageData(int stageLevel)
    {
        stageInfo = Managers.Data.GetStageInfo(stageLevel);
    }

    public void RestartStage()
    {

    }
}
