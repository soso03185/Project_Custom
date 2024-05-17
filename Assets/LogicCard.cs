using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LogicCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    LogicUIManager m_LogicUIMgr;
    ScrollRect m_ParentScrollRect;
    float m_RectWidth = 0f;
    bool m_IsForParent = false;

    public void Start()
    {
        m_LogicUIMgr = GameObject.FindWithTag("LogicUIManager").GetComponent<LogicUIManager>();
        m_ParentScrollRect = GameObject.FindWithTag("LogicUIManager").GetComponent<ScrollRect>();
        m_RectWidth = GameObject.FindWithTag("LogicUIManager").GetComponent<RectTransform>().rect.width;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_IsForParent = Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y);

        if (m_IsForParent)
        {
            m_LogicUIMgr.OnBeginDrag(eventData);
            m_ParentScrollRect.OnBeginDrag(eventData);
        }
        else
        {
            Debug.Log("Card OnBeginDrag");
            m_LogicUIMgr.m_DummyCard.SetActive(true);
            m_LogicUIMgr.m_DummyCard.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (m_IsForParent)
        {
            m_LogicUIMgr.OnDrag(eventData);
            m_ParentScrollRect.OnDrag(eventData);
        }
        else
        {
            m_LogicUIMgr.m_DummyCard.transform.position = eventData.position;

            if (eventData.position.x >= m_RectWidth)
            {
                Debug.Log("Scrolling to Right");
            }
            if (eventData.position.x <= 0)
            {
                Debug.Log("Scrolling to Left");
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (m_IsForParent)
        {
            m_LogicUIMgr.OnEndDrag(eventData);
            m_ParentScrollRect.OnEndDrag(eventData);
        }
        else
        {
            Debug.Log("Card OnEndDrag");
            m_LogicUIMgr.m_DummyCard.SetActive(false);
        }
    }
}
