using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionLogicManager : MonoBehaviour
{
    public Skill[] m_actionLogic = new Skill[8];
    public ActionLogicUI_Line[] m_actionLogicUI = new ActionLogicUI_Line[8];
    public float m_convertingTime;

    public int m_currentIndex = 0;

    public Skill this[int i]
    {
        get { return m_actionLogic[i]; }
        set { m_actionLogic[i] = value; }
    }

    void Awake()
    {
        for (int i = 0; i < m_actionLogic.Length; i++)
        {
            m_actionLogic[i] = Instantiate(m_actionLogic[i]);
        }
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

    public void Reset()
    {
        StopAllCoroutines();
        m_currentIndex = 0;
    }
}
