using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowStep : Skill
{
    private const string KeyIsMove = "IsRun";
    public override IEnumerator Play(Player player)
    {
        yield return new WaitForSeconds(1f);
        GameObject target;
        m_animator = player.GetComponent<Animator>();
        m_animator.SetBool(KeyIsMove, true);

        while (true)
        {
            target = Managers.Monsters.GetFarthestMonster();

            if (target != null)
            {
                break;
            }

            yield return null;
        }

        Vector3 direction = (target.transform.position - player.transform.position).normalized;

        direction.y = 0;

        player.transform.rotation = Quaternion.LookRotation(direction);

        player.transform.position = target.transform.position;

        m_animator.SetBool(KeyIsMove, false);

        player.SkillEnd();
    }


}
