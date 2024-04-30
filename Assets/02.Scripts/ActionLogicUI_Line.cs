using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ActionLogicUI_Line : SkillIcon
{
    public int m_actionIndex;
    public ActionLogicManager m_manager;
    // Start is called before the first frame update
    void Awake()
    {
        m_Skill = m_manager.m_actionLogic[m_actionIndex];

        SetMembers();

        UISet();
    }

    public IEnumerator UIMove()
    {
        Vector3 currentPosition = m_rectTransform.position;

        float elapsedTime = 0.0f;

        while (true)
        {
            elapsedTime += Time.deltaTime / m_manager.m_convertingTime;
            m_rectTransform.position = Vector3.Lerp(currentPosition, currentPosition - new Vector3(200, 0, 0), elapsedTime);

            if(elapsedTime >= 1)
                break;

            yield return null;
        }

        UISet();

        if (m_actionIndex == m_manager.m_currentIndex - 1 || m_actionIndex == m_manager.m_currentIndex + 7)
        {
            m_rectTransform.position = new Vector3(1500, 50, 0);
        }
    }

    void UISet()
    {
        if (m_actionIndex == m_manager.m_currentIndex)
        {
            m_rectTransform.sizeDelta = new Vector2(200, 200);
            m_image.color = new Color(1, 1, 1, 1);
        }
        else
        {
            m_rectTransform.sizeDelta = new Vector2(150, 150);
            m_image.color = new Color(1, 1, 1, 0.5f);
        }
    }
}
