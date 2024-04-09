using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public static int size = 10;
    public Skill[] container = new Skill[size];

    private bool isSkillEnd = false;

    private int currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isSkillEnd)
        {
            currentIndex = (currentIndex + 1) % container.Length;
            Debug.Log(currentIndex);
        }
        ActivateSkill(container[currentIndex]);
    }

    public void ActivateSkill(Skill skill)
    {
        isSkillEnd = skill.Play(this);
    }
}
