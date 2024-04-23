using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int m_coin = 0;
    private int m_level = 0;
    private int m_exp = 0;

    [SerializeField]
    private TextMeshProUGUI m_coinText;

    [SerializeField] private TextMeshProUGUI m_expPercent;
    [SerializeField] private TextMeshProUGUI m_levelText;

    public List<int> m_expDummy = new List<int>();
    [SerializeField] private Slider m_expSlider;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            m_coin += 100;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            m_exp += 10;
        }

        if (m_exp >= m_expDummy[m_level])
        {
            m_exp -= m_expDummy[m_level];
            m_level++;
        }

        m_coinText.text = m_coin.ToString();
        m_expPercent.text = (m_exp / (float)m_expDummy[m_level] * 100) + "%";
        m_levelText.text = m_level.ToString();
        m_expSlider.value = m_exp / (float)m_expDummy[m_level];
    }
}
