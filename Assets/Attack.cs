using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Skill
{
    private const string KeyIsAttack = "IsAttack01";
    public GameObject player;
    public override bool Play(Player gameObject)
    {
        transform.position = player.transform.position;
        Animator animator = gameObject.GetComponent<Animator>();
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
        if (collider.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit");

        }
    }
}
