using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static Define;

public class DemoMonster : MonoBehaviour
{
    [SerializeField]
    public MonsterState monsterState = MonsterState.spawn;

    [SerializeField]
    public float m_hp {  get; set; }

    [SerializeField]
    private float monsterHP;
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

    public GameManager manager;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();

        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        manager.AddMonster(this);
    }

    // 활성화 될 때마다 실행됨
    private void OnEnable()
    {
        ResetMonster();
    }

    void Update()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        //GetComponent<Rigidbody>(). = Vector3.zero;

        switch (monsterState)
        {
            case MonsterState.spawn:
                StartCoroutine(UpdateSpawn());
                break;
            case MonsterState.move:
                UpdateBase();
                UpdateMove();
                break;
            case MonsterState.attack:
                UpdateAttack();
                UpdateBase();
                break;
            case MonsterState.hit:
                StartCoroutine(UpdateHit());
                UpdateBase();
                break;
            case MonsterState.dead:
                StartCoroutine(UpdateDead());
                break;
        }
    }

    void UpdateBase()
    {
        if (monsterHP == 0)
        {
            ChangeState(MonsterState.dead);
        }
    }

    IEnumerator UpdateSpawn()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Spawn"))
        {
            LookAtPlayer();
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
            ChangeState(MonsterState.move);
        }
    }

    void UpdateMove()
    {
        this.gameObject.GetComponent<Rigidbody>().mass = 1;
        transform.position += LookAtPlayer() * moveSpeed * Time.deltaTime;
        if (GetDistance(target.position, transform.position) < attackRange)
        {
            ChangeState(MonsterState.attack);
        }
    }

    void UpdateAttack()
    {
        anim.SetBool("isAttack", true);
        this.gameObject.GetComponent<Rigidbody>().mass = 10000f;
        LookAtPlayer();

        if (GetDistance(target.position, transform.position) > attackRange)
        {
            anim.SetBool("isAttack", false);
            ChangeState(MonsterState.move);
        }
    }

    IEnumerator UpdateHit()
    {
        anim.SetBool("isHit", true);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        ReturnFromHit();
    }

    IEnumerator UpdateDead()
    {
        // 재화 및 경험치 얻는 처리?
        anim.SetBool("isDead", true);
        yield return new WaitForSeconds(0.1f);

        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length * 5);

        // 오브젝트 풀 통해서 관리하기
        //ObjectPool.Instance.ReturnObject(this.gameObject);
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
                ChangeState(MonsterState.hit);
                IsDamaged(100);
            }
        }
    }

    // JaeHyeon
    void IsDamaged(float damage)
    {
        monsterHP -= ((int)damage - defense);
    }

    void ReturnFromHit()
    {
        anim.SetBool("isHit", false);
        if (GetDistance(target.position, transform.position) < attackRange)
        {
            ChangeState(MonsterState.attack);
        }
        else
        {
            ChangeState(MonsterState.move);
        }
    }

    void ResetMonster()
    {
        ChangeState(MonsterState.spawn);
        //monsterHP = 100;
    }

}
