using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static Define;

public class DemoMonster : IMonster
{
    public MonsterState monsterState = MonsterState.spawn;

    public float monsterHP;
    private float prevMonsterHP;
    public float MonsterHP
    {
        get 
        { 
            return monsterHP;
        }
        set
        {
            if (monsterState != MonsterState.spawn)
                monsterHP = value;
        }
    }

    public float attack;
    public float defense;
    public float attackSpeed;
    public float attackRange;

    public int moveSpeed;

    public Transform target;
    Animator anim;

    public MonsterManager manager;

    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();

        manager = GameObject.Find("MonsterManager").GetComponent<MonsterManager>();
        manager.AddMonster(this);
    }

    void Update()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        switch (monsterState)
        {
            case MonsterState.move:
                UpdateBase();
                UpdateMove();
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
    }

    public void OnDestroy()
    {
        manager.DeleteMonster(this);
    }

    void UpdateBase()
    {
        if (monsterHP < prevMonsterHP)
        {
            ChangeState(MonsterState.hit);
        }

        prevMonsterHP = monsterHP;

        if (monsterHP == 0)
        {
            ChangeState(MonsterState.dead);
        }

    }

    void UpdateSpawn()
    {
        ChangeState(MonsterState.move);
    }

    void UpdateMove()
    {
        anim.Play("Walk");
        this.gameObject.GetComponent<Rigidbody>().mass = 1;
        transform.position += LookAtPlayer() * moveSpeed * Time.deltaTime;

        if (GetDistance(target.position, transform.position) < attackRange)
        {
            ChangeState(MonsterState.attack);
        }
    }

    void UpdateAttack()
    {
        anim.Play("Attack");

        this.gameObject.GetComponent<Rigidbody>().mass = 1000f;
        LookAtPlayer();

        if (GetDistance(target.position, transform.position) > attackRange)
        {
            ChangeState(MonsterState.move);
        }
    }

    void UpdateHit()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99)
        {
            anim.Play("Hit");
            ChangeState(MonsterState.move);
        }
    }

    void UpdateDead()
    {
        // 재화 및 경험치 얻는 처리?

        this.gameObject.SetActive(false);
        isDead = true;
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
