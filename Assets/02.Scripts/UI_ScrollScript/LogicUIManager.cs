using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LogicUIManager : MonoBehaviour
{
    public GameObject m_Contents = null;
    public GameObject m_LogicPrefab = null;
    public int m_LogicCount = 5;

    public List<LogicCard> m_LogicCards = new List<LogicCard>();
    public List<LogicCard> m_LogicDatas = new List<LogicCard>();

    [HideInInspector] public GameObject m_DummyCard;

    public void Start()
    {
        m_DummyCard = Instantiate(m_LogicPrefab, this.transform);
        m_DummyCard.SetActive(false);

        // 로직 카드 첫 생성
        for (int i = 0; i < m_LogicCount; i++)
        {
            LogicCard go = Instantiate(m_LogicPrefab, m_Contents.transform).GetComponent<LogicCard>() ;
            m_LogicCards.Add(go);
        }
    }

    void Update()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Parent Begin");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Parent Drag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Parent End");
    }

    public void Refresh()
    {

    }
}
