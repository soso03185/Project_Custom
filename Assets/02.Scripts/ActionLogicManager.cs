using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionLogicManager : MonoBehaviour
{
    public Skill[] m_actionLogic = new Skill[8];
    public float convertingTime;

    public int m_currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextAction();
        }
    }

    Skill GetCurrentSkill()
    {
        return m_actionLogic[m_currentIndex];
    }

    void NextAction()
    {
        m_currentIndex = (m_currentIndex + 1) % m_actionLogic.Length;

    }
}
