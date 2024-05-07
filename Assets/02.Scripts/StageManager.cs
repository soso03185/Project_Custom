using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private int m_currentStage = 1;
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI m_stageText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_stageText.text = m_currentStage.ToString();
    }
}
