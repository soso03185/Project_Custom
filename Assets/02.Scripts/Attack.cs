using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IMonster;

public class Attack : Skill
{
    private const string KeyIsAttack = "IsAttack01";
    public GameObject player;

    public IMonster monster; 

    public void Start()
    {
        //playerObject = GameObject.FindGameObjectWithTag("Player");
        monster.HP = 100;
    }
       
    public override bool Play(Player playerObject)
    {
        
        Animator animator = playerObject.GetComponent<Animator>();
        animator.SetBool(KeyIsAttack, true);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01"))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                this.transform.position = playerObject.transform.position;
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
