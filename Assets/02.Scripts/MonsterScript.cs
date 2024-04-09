using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MonsterScript : MonoBehaviour
{
    public enum State { spawn, move, attack, dead };

    public State monsterState = State.spawn;

    public int monsterHP;
    public int attack;
    public int defense;
    public int attackSpeed;

    private NavMeshAgent nvAgent;

    public Transform target;
    Animator anim;

    private bool isDead = false;
    private bool isSpawned = false;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();

        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();

        StartCoroutine(this.CheckState());
        StartCoroutine(this.CheckStateForAction());

    }

    IEnumerator CheckState()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(0.2f);

            float dist = Vector3.Distance(target.position, transform.position);

            if (dist < nvAgent.stoppingDistance)
            {
                monsterState = State.attack;
            }
            else
            {
                monsterState = State.move;
            }
        }
    }

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
        isSpawned = anim.GetCurrentAnimatorStateInfo(0).IsName(animName);
        return isSpawned;
    }

    void UpdateSpawn()
    {
        nvAgent.isStopped = true;
        anim.SetBool("isSpawn", true);
    }

    void UpdateMove()
    {
        nvAgent.isStopped = false;
        nvAgent.destination = target.position;
        anim.SetBool("isAttack", false);
    }

    void UpdateAttack()
    {
        anim.SetBool("isAttack", true);
    }

}
