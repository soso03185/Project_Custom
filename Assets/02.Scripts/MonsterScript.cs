using System.Collections;
using System.Collections.Generic;
using AssetKits.ParticleImage.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static Define;

public class DemoMonster : MonoBehaviour
{
    [SerializeField]
    public MonsterState monsterState = MonsterState.spawn;

    public float m_monsterHp;
    
    //joohong
    private GameObject m_canvas;

    //joohong
    [SerializeField]
    private GameObject monsterHPBar;

    private GameObject m_HpBar;

    public Transform target;
    Animator anim;

    public DataManager.MonsterInfo monsterInfo;
    public int monsterID;

    public MonsterManager manager;
    
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
                UpdateBase();
                UpdateAttack();
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
        if (monsterInfo.Hp == 0)
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
        //this.gameObject.SetActive(false);

        // 오브젝트 풀 통해서 관리하기
        Managers.Pool.GetPool(this.gameObject.name).ReturnObject(this.gameObject);
        m_HpBar.SetActive(false);

        // 초기화
        monsterInfo.Hp = Managers.Data.UGS_Data.m_MonsterDataDic[monsterID].Hp;

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
        monsterInfo.Hp -= ((int)damage - monsterInfo.Defense);
    }

    void ReturnFromHit()
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

    void ResetMonster()
    {
        ChangeState(MonsterState.spawn);
        
        //joohong
        m_HpBar.SetActive(true);
    }

}
