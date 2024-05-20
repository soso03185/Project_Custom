using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LogicSettingUI_Slot : Slot
{
    [SerializeField] private ActionLogicManager m_logicManager;

    [SerializeField] private int m_slotIndex;

    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);

        //m_logicManager.m_actionLogic[m_slotIndex] =IconDrag.m_beginDraggedIcon.GetComponent<LogicSetttingUI_SkillIcon>().m_Skill;

    }

    public void Set()
    {
        if (this.transform.childCount == 0)
        {
            return;
        }
        Skill skill = GetComponentInChildren<LogicSetttingUI_SkillIcon>().m_Skill;
        m_logicManager[m_slotIndex] = skill;
    }
}
