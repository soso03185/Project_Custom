using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public ActionLogicManager m_logicManager;

    private bool isSkillEnd = false;

    private int currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ActivateSkill(m_logicManager.m_actionLogic[currentIndex]);
    }

    public void ActivateSkill(Skill skill)
    {
        StartCoroutine(skill.Play(this));
    }
}
