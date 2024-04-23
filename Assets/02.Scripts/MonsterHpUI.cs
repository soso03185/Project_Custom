using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpUI : MonoBehaviour
{
    private GameObject m_monster;
    private RectTransform m_rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        m_rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_monster != null)
        {
            m_rectTransform.position =
                Camera.main.WorldToScreenPoint(m_monster.transform.position) + new Vector3(0, 100, 0);
        }

        GetComponent<Slider>().value = m_monster.GetComponent<DemoMonster>().MMonsterHp / 100f;
    }

    public void SetMonster(GameObject monster)
    {
        m_monster = monster;
    }
}
