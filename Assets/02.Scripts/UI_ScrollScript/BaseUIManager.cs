using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseUIManager : MonoBehaviour
{
    public TextMeshProUGUI m_PowerAmount;
    public TextMeshProUGUI m_GoldAmount;
    public TextMeshProUGUI m_DiaAmount;
    public TextMeshProUGUI m_ExpAmount;
    public TextMeshProUGUI m_StageCount;

    void Start()
    {
        m_PowerAmount.text = "Text";
        m_GoldAmount.text = "Text";
        m_DiaAmount.text = "Text";
        m_ExpAmount.text = "Text";
        m_StageCount.text = "Text";
    }

    void Update()
    {
        Refresh();
    }

    public void Refresh()
    {

    }
}
