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
       
    public override bool Play(Player playerObject)
    {
        this.transform.position = playerObject.transform.position;
        Animator animator = playerObject.GetComponent<Animator>();
        animator.SetBool(KeyIsAttack, true);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01"))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                animator.SetBool(KeyIsAttack, false);
                return true;
            }
        }
        return false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            Debug.Log("Hit");
        }
    }
}
