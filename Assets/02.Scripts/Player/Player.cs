using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public ActionLogicManager m_logicManager;
    // Start is called before the first frame update
    void Start()
    {
        ActivateSkill(m_logicManager.m_actionLogic[m_logicManager.m_currentIndex]);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ActivateSkill(Skill skill)
    {
        StartCoroutine(skill.Play(this));
    }

    public void SkillEnd()
    {
        m_logicManager.NextAction();
        if (m_logicManager.m_actionLogic[m_logicManager.m_currentIndex])
        {
            ActivateSkill(m_logicManager.m_actionLogic[m_logicManager.m_currentIndex]);
        }
        else
        {
            SkillEnd();
        }
    }
}
