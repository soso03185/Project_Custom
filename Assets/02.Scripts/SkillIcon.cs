using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class SkillIcon : MonoBehaviour
{
    protected RectTransform m_rectTransform;

    protected Image m_image;

    public Skill m_Skill;

    public void SetMembers()
    {
        m_rectTransform = GetComponent<RectTransform>();
        m_image = GetComponent<Image>();
        m_image.sprite = m_Skill.image;
    }

}
