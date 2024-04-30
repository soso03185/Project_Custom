using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    GameObject Icon()
    {
        if (transform.childCount > 0)
        {
            return transform.GetChild(0).gameObject;
        }
        else
        {
            return null;
        }
    }
    public virtual void OnDrop(PointerEventData eventData)
    {
        if (Icon() == null)
        {
            IconDrag.m_beginDraggedIcon.transform.SetParent(transform);
            IconDrag.m_beginDraggedIcon.transform.position = transform.position;
        }
    }
}
