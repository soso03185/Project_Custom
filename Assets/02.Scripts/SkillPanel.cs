using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanel : MonoBehaviour
{
    public SkillPool m_skillPool;

    public LogicSetttingUI_SkillIcon m_skillIcon;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < m_skillPool.m_skills.Count; i++)
        {
            Instantiate(m_skillIcon).transform.SetParent(transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
