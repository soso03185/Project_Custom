using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class MonsterScript : MonoBehaviour
{
    public enum State { spawn, move, attack, dead };

    public State monsterState = State.spawn;

    public int speed;

    public int Speed
    { 
        get { return speed; }
        set
        {
            speed = value;
        } 
    }

    public int monsterHP;
    public int attack;
    public int defense;
    public int attackSpeed;
    public int attackRange;

    public int moveSpeed;
    private NavMeshAgent nvAgent;

    public Transform target;
    Animator anim;

    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();

        //nvAgent = this.gameObject.GetComponent<NavMeshAgent>();

        //StartCoroutine(this.CheckState());
        StartCoroutine(this.CheckStateForAction());
    }

    //IEnumerator CheckState()
    //{
    //    while (!isDead)
    //    {
    //        yield return new WaitForSeconds(0.2f);

    //        float dist = Vector3.Distance(target.position, transform.position);

    //        if (dist < attackRange)
    //        {
    //            monsterState = State.attack;
    //        }
    //        else
    //        {
    //            monsterState = State.move;
    //        }
    //    }
    //}

    IEnumerator CheckStateForAction()
    {
        while (!isDead)
        {
            switch (monsterState)
            {
                case State.spawn:
                    UpdateSpawn();
                    break;
                case State.move:
                    UpdateMove();
                    break;
                case State.attack:
                    UpdateAttack();
                    break;
            }
            yield return null;
        }
    }

    bool IsAnimationPlaying(string animName)
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(animName);
    }

    void UpdateSpawn()
    {
        anim.SetTrigger("isSpawn");
        ChangeState(State.move);
    }

    void UpdateMove()
    {
        //nvAgent.isStopped = false;
        //nvAgent.destination = target.position;

        Vector3 followDirection = (target.position - transform.position).normalized;

        followDirection.y = 0f;

        transform.rotation = Quaternion.LookRotation(followDirection);

       // float distance = (target.position - transform.position).magnitude;
        transform.position += followDirection * moveSpeed * Time.deltaTime;

        float dist = Vector3.Distance(target.position, transform.position);

        if (dist < attackRange)
        {
            ChangeState(State.attack);
            anim.SetBool("isAttack", true);
        }

    }

    void UpdateAttack()
    {
        float dist = Vector3.Distance(target.position, transform.position);

        if (dist > attackRange)
        {
            ChangeState(State.move);
            anim.SetBool("isAttack", false);
        }

    }

    void ChangeState(State state)
    {
        monsterState = state;
    }


    private void OnTriggerEnter(Collider other)
    {
        // JaeHyeon
        {
            if (other.gameObject.CompareTag("Skill"))
            {
                Debug.Log("Hit");
                IsDamaged(1);
            }
        }
    }

    // JaeHyeon
    void IsDamaged(float damage)
    {
        monsterHP -= ((int)damage - defense);
    }
}
