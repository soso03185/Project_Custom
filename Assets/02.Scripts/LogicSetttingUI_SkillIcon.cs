using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicSetttingUI_SkillIcon : SkillIcon
{
    [SerializeField] private SkillPool m_skillPool;

    private static int m_index = 0;

    void Start()
    {
        m_skillPool = GameObject.Find("SkillPool").GetComponent<SkillPool>();
        m_Skill = m_skillPool.m_skills[m_index];

        SetMembers();
        m_rectTransform.localPosition = new Vector3(-350 + (m_index % 5) * 175, 250 - 175 * (m_index / 5), 0);
        m_rectTransform.sizeDelta = new Vector2(150, 150);
        m_index++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
