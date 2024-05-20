using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionLogicManager : MonoBehaviour
{
    public Skill[] m_actionLogic = new Skill[8];
    public ActionLogicUI_Line[] m_actionLogicUI = new ActionLogicUI_Line[8];
    public float m_convertingTime;

    public int m_currentIndex = 0;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (var skill in m_actionLogic)
        {
            Instantiate(skill);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    Skill GetCurrentSkill()
    {
        return m_actionLogic[m_currentIndex];
    }

    public void NextAction()
    {
        m_currentIndex = (m_currentIndex + 1) % m_actionLogic.Length;
        foreach (var UI in m_actionLogicUI)
        {
            UI.StartCoroutine(UI.UIMove());
        }
    }
}
