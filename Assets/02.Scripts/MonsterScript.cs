using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static Define;

public class MonsterScript : MonoBehaviour
{
    MonsterState monsterState = MonsterState.spawn;
   
    public float monsterHP;
    private float prevMonsterHP;
    public float MonsterHP
    { get { return monsterHP; } 
      set { 

            if(monsterState != MonsterState.spawn)
            {
                monsterHP = value;
            }
        }    
    }

    public float attack;
    public float defense;
    public float attackSpeed;
    public float attackRange;

    public int moveSpeed;
    //private NavMeshAgent nvAgent;

    public Transform target;
    Animator anim;

    public GameManager manager;

    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();

        //nvAgent = this.gameObject.GetComponent<NavMeshAgent>();


        //StartCoroutine(this.CheckState());
        StartCoroutine(this.CheckStateForAction());

        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        manager.AddMonster(this);
    }

    public void OnDestroy()
    {
        manager.DeleteMonster(this);
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
                case MonsterState.spawn:
                    UpdateSpawn();
                    break;
                case MonsterState.move:
                    UpdateMove();
                    UpdateBase();
                    break;
                case MonsterState.attack:
                    UpdateAttack();
                    UpdateBase();
                    break;
                case MonsterState.hit:
                    UpdateHit();
                    break;
                case MonsterState.dead:
                    UpdateDead();
                    break;
            }
            yield return null;
        }
    }

    void UpdateBase()
    {
        if(monsterHP < prevMonsterHP)
        {
            ChangeState(MonsterState.hit);
            anim.SetBool("isHit", true);
        }

        prevMonsterHP = monsterHP;

        if (monsterHP == 0)
        {
            ChangeState(MonsterState.dead);
            anim.SetBool("isDead", true);
        }

    }

    void UpdateSpawn()
    {
        anim.SetTrigger("isSpawn");
        ChangeState(MonsterState.move);
    }

    void UpdateMove()
    {
        //nvAgent.isStopped = false;
        //nvAgent.destination = target.position;

        transform.position += LookAtPlayer() * moveSpeed * Time.deltaTime;

        if (GetDistance(target.position, transform.position) < attackRange)
        {
            ChangeState(MonsterState.attack);
            anim.SetBool("isAttack", true);
        }
    }

    void UpdateAttack()
    {
        LookAtPlayer();

        if (GetDistance(target.position, transform.position) > attackRange)
        {
            ChangeState(MonsterState.move);
            anim.SetBool("isAttack", false);
        }
    }

    void UpdateHit()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99)
        {
            anim.SetBool("isHit", false);
        }
    }

    void UpdateDead()
    {
        // 재화 및 경험치 얻는 처리?
    }

    void ChangeState(MonsterState state)
    {
        monsterState = state;
    }

    Vector3 LookAtPlayer()
    {
        Vector3 followDirection = (target.position - transform.position).normalized;

        followDirection.y = 0f;

        transform.rotation = Quaternion.LookRotation(followDirection);

        return followDirection;
    }

    float GetDistance(Vector3 pos1, Vector3 pos2)
    {
        return Vector3.Distance(pos1, pos2);
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
