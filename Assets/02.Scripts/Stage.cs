using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Stage : MonoBehaviour
{
    // 현재 스테이지

    // 스테이지 레벨
    public int stageLevel;
    // 스테이지 레벨 UI
    public TextMeshProUGUI stageLevelUI;
    // 그 스테이지의 몬스터 수
    public int maxMonsterCount;

    // 몬스터 종류

    // 보스 스테이지인지의 여부
    public bool isBossStage;
    // 보스 몬스터 프리팹
    //public GameObject boss;
    // 보스 스테이지 한정 제한 시간
    public float timeLimit;
    // 제한 시간 UI

    // 클리어 성공 여부
    public bool isStageClear;

    // 배경 리소스 프리팹

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
