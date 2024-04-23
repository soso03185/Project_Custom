using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ActionLogicUI_Line : MonoBehaviour
{
    public int m_actionIndex;
    public ActionLogicManager m_manager;
    private RectTransform m_transform;
    private Image m_image;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().sprite = m_manager.m_actionLogic[m_actionIndex].image;

        m_transform = GetComponent<RectTransform>();
        m_image = GetComponent<Image>();

        UISet();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    StartCoroutine(UIMove());
        //}

       
    }

    public IEnumerator UIMove()
    {
        Vector3 currentPosition = m_transform.position;

        float elapsedTime = 0.0f;

        while (true)
        {
            elapsedTime += Time.deltaTime / m_manager.m_convertingTime;
            m_transform.position = Vector3.Lerp(currentPosition, currentPosition - new Vector3(200, 0, 0), elapsedTime);

            if(elapsedTime >= 1)
                break;

            yield return null;
        }

        UISet();
        if (m_actionIndex == m_manager.m_currentIndex - 1 || m_actionIndex == m_manager.m_currentIndex + 7)
        {
            m_transform.position = new Vector3(1500, 50, 0);
        }
    }

    void UISet()
    {
        if (m_actionIndex == m_manager.m_currentIndex)
        {
            m_transform.sizeDelta = new Vector2(200, 200);
            m_image.color = new Color(1, 1, 1, 1);
        }
        else
        {
            m_transform.sizeDelta = new Vector2(150, 150);
            m_image.color = new Color(1, 1, 1, 0.5f);
        }
    }
}
