using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Skill
{
    private const string KeyIsAttack = "IsAttack01";
    public void Start()
    {
    }

    public override IEnumerator Play(Player playerObject)
    {
        float elapsedTime = 0;
        m_animator = playerObject.GetComponent<Animator>();
        m_animator.SetBool(KeyIsAttack, true);

        while (!m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01"))
        {
            yield return null;
        }

        while (m_animator.GetCurrentAnimatorStateInfo(0).length <= elapsedTime)
        {
            yield return null;
        }

        this.GetComponent<Collider>().enabled = true;
        this.transform.position = playerObject.transform.position;
        yield return true;

        this.GetComponent<Collider>().enabled = false;
        m_animator.SetBool(KeyIsAttack, false);

        playerObject.SkillEnd();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            Debug.Log("Hit");
        }
    }
}
