using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Skill
{
    private const string KeyIsAttack = "IsAttack01";
    public GameObject player;
    public void Start()
    {
        //playerObject = GameObject.FindGameObjectWithTag("Player");
    }
       
    public override IEnumerator Play(Player playerObject)
    {
        m_animator = playerObject.GetComponent<Animator>();
        m_animator.SetBool(KeyIsAttack, true);
        if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01"))
        {
            if (m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                this.transform.position = playerObject.transform.position;
                m_animator.SetBool(KeyIsAttack, false);
                yield return true;
            }
        }
        yield return null;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            Debug.Log("Hit");
        }
    }
}
