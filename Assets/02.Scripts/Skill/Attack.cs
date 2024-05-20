using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Skill
{
    private const string KeyIsAttack = "IsAttack01";
    public void Start()
    {
    }

    public int atkCount;
    public float atk;
    public float atkRange;
    public float castingTime;
    public float criticalChance;
    public float criticalMultiplier;

    public override IEnumerator Play(Player playerObject)
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(castingTime);
        float elapsedTime = 0;
        m_animator = playerObject.GetComponent<Animator>();
        m_animator.SetBool(KeyIsAttack, true);

        for(int i = 0; i < atkCount; i++)
        {
            while (!m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01"))
            {
                yield return null;
            }

            while (m_animator.GetCurrentAnimatorStateInfo(0).length >= elapsedTime)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            GetComponent<BoxCollider>().enabled = true;
            GetComponent<BoxCollider>().size *= atkRange;
            transform.position = playerObject.transform.position;
            transform.rotation = playerObject.transform.rotation;
        }
        //출혈 효과 부여

        yield return null;

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
