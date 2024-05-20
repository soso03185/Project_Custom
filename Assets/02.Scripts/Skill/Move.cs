using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Move : Skill
{
    public MonsterManager m_monsterManager;

    //public Sprite image;

    public float playerSpeed;
    public float attackRange;
    private float _targetDistance = 0;

    private const string KeyIsMove = "IsRun";

    void Start()
    {
        m_monsterManager = Managers.Monsters;
    }
    public override IEnumerator Play(Player playerObject)
    {
        yield return new WaitForSeconds(1f);
        GameObject target;
        m_animator = playerObject.GetComponent<Animator>();
        m_animator.SetBool(KeyIsMove, true);

        while (true)
        {
            target = m_monsterManager.GetNearestMonster(playerObject);

            if (target != null)
            {
                break;
            }

            yield return null;
        }

        while (true)
        {
            Vector3 direction = (target.transform.position - playerObject.transform.position).normalized;

            direction.y = 0;

            if (direction != Vector3.zero)
            {
                playerObject.transform.rotation = Quaternion.LookRotation(direction);
            }

            _targetDistance = Vector3.Distance(target.transform.position, playerObject.transform.position);

            if (_targetDistance <= attackRange)
            {
                break;
            }

            playerObject.transform.position += direction * playerSpeed * Time.deltaTime;

            yield return null;
        }
        m_animator.SetBool(KeyIsMove, false);
        
        playerObject.SkillEnd();
    }
}
