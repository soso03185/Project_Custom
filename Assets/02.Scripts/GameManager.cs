using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isPlayerDead;
    public bool isStageClear;

    public int currentStage;

    private DataManager.UserInfo userInfo;
    private DataManager.StageInfo stageInfo;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;    
        }
        else
        {
            Debug.Log("GameManager already exists");
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void Init()
    {
        userInfo = Managers.Data.GetUserInfo(1001);
        currentStage = userInfo.StageLevel;
    }


    // Update is called once per frame
    void Update()
    {
        if(GameOver())
        {
            Managers.Stage.RestartStage();
        }
        else if(StageClear())
        {
            Managers.Stage.LevelUpStage();
        }
    }

    public bool GameOver()
    {
        if(userInfo.Hp <= 0)
        {
            isPlayerDead = true;
            return isPlayerDead;
        }
        else
            return false;
    }

    public bool StageClear()
    {
        return false;
    }
}
