using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Stage : MonoBehaviour
{
    // ���� ��������

    // �������� ����
    public int stageLevel;
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

    // Start is called before the first frame update
    void Start()
    {
        stageInfo = Managers.Data.GetStageInfo(stageLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void GetStageData(int stageLevel)
    {

    }

    public void LevelUpStage()
    {
        if (isStageClear)
        {
            stageLevel++;
            stageLevelUI.GetComponent<TextMeshProUGUI>().text = stageLevel.ToString();
        }
        else
        {

        }
    }

    public void RestartStage()
    {

    }
}
