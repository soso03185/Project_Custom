using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;

public class IconDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject m_beginDraggedIcon;

    private Vector3 m_startPosition;

    [SerializeField] private Transform m_onDragParent;

    [SerializeField] private Transform m_startParent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_beginDraggedIcon = gameObject;

        m_startPosition = transform.position;
        m_startParent = transform.parent;

        GetComponent<CanvasGroup>().blocksRaycasts = false;

        //transform.SetParent(m_onDragParent);
        m_onDragParent = transform.parent;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_beginDraggedIcon = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (transform.parent == m_onDragParent)
        {
            transform.position = m_startPosition;
            transform.SetParent(m_startParent);
        }
    }
}
