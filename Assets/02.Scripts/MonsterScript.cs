using System.Collections;
using System.Collections.Generic;
using AssetKits.ParticleImage.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static DataManager;
using static Define;

public class DemoMonster : MonoBehaviour
{
    [SerializeField]
    public MonsterState monsterState = MonsterState.spawn;

    //joohong
    private GameObject m_canvas;

    //joohong
    [SerializeField]
    private GameObject monsterHPBar;

    private GameObject m_HpBar;

    public Transform target;
    Animator anim;

    public int monsterID;

    public MonsterManager manager;

    public DataManager.MonsterInfo monsterInfo;

    
    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();

        //joohong
        m_canvas = GameObject.Find("Canvas");

        m_HpBar = Instantiate(monsterHPBar, Vector3.zero, Quaternion.identity, m_canvas.transform);
        m_HpBar.GetComponent<MonsterHpUI>().SetMonster(this.gameObject);

    }

    // 활성화 될 때마다 실행됨
    private void OnEnable()
    {
        ResetMonster();
    }
    private void Start()
    {
        monsterInfo = Managers.Data.GetMonsterInfo(monsterID);
    }

    void Update()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        UpdateBase();
        switch (monsterState)
        {
            case MonsterState.spawn:
                UpdateSpawn();
                break;
            case MonsterState.move:
                UpdateMove();
                break;
            case MonsterState.attack:
                UpdateAttack();
                break;
            case MonsterState.hit:
                UpdateHit();
                break;
            case MonsterState.dead:
                UpdateDead();
                break;
        }
    }

    void UpdateBase()
    {
        if (monsterInfo.Hp <= 0)
        {
            ChangeState(MonsterState.dead);
        }
    }

    void UpdateSpawn()
    {
        LookAtPlayer();
        
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0)
        {
            return;
        }
        else
        {
            anim.SetTrigger("isSpawn");
            ChangeState(MonsterState.move);
        }
    }

    void UpdateMove()
    {

        this.gameObject.GetComponent<Rigidbody>().mass = 1;
        transform.position += LookAtPlayer() * monsterInfo.MoveSpeed * Time.deltaTime;
        if (GetDistance(target.position, transform.position) < monsterInfo.AttackRange)
        {
            ChangeState(MonsterState.attack);
        }
    }

    void UpdateAttack()
    {
        LookAtPlayer();
        anim.SetBool("isAttack", true);
        this.gameObject.GetComponent<Rigidbody>().mass = 10000f;

        if (GetDistance(target.position, transform.position) > monsterInfo.AttackRange)
        {
            anim.SetBool("isAttack", false);
            ChangeState(MonsterState.move);
        }
    }

    void UpdateHit()
    {
        anim.SetBool("isHit", true);
    }


    void UpdateDead()
    {
        anim.SetBool("isDead", true);
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
                ChangeState(MonsterState.hit);
                if (other.gameObject.GetComponent<Attack>().criticalChance > Random.value * 100)
                {
                    IsDamaged(other.gameObject.GetComponent<Attack>().atk * other.gameObject.GetComponent<Attack>().criticalMultiplier);
                }
                else
                {
                    IsDamaged(other.gameObject.GetComponent<Attack>().atk);
                }
            }
        }
    }

    // JaeHyeon
    void IsDamaged(float damage)
    {
        monsterInfo.Hp -= ((int)damage - monsterInfo.Defense);
    }


    void ResetMonster()
    {
        ChangeState(MonsterState.spawn);
        
        //joohong
        m_HpBar.SetActive(true);
    }


    void AnimEventSpawn()
    {
        anim.SetTrigger("isSpawn");
        ChangeState(MonsterState.move);
    }

    // 피격 판정 후 상태 처리 이벤트 함수
    void AnimEventHit()
    {
        anim.SetBool("isHit", false);
        if (GetDistance(target.position, transform.position) < monsterInfo.AttackRange)
        {
            ChangeState(MonsterState.attack);
        }
        else
        {
            ChangeState(MonsterState.move);
        }
    }

    // 몬스터 삭제 이벤트 함수
    void AnimEventDead()
    {
        Managers.Stage.deadMonsterCount++;
        ChangeState(MonsterState.spawn);
        
        //joohong
        m_HpBar.SetActive(true);
    }
}
