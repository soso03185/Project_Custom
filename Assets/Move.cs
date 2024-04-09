using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Move : Skill
{
    public float distance = 3;
    private float elapsedDistance = 0;

    private const string KeyIsMove = "IsRun";
    public override bool Play(Player gameObject)
    {
        Animator animator = gameObject.GetComponent<Animator>();
        animator.SetBool(KeyIsMove, true);

        elapsedDistance += Time.deltaTime;
        gameObject.transform.Translate(Vector3.forward * Time.deltaTime);
        if (elapsedDistance >= distance)
        {
            animator.SetBool(KeyIsMove, false);
            elapsedDistance = 0;
            return true;
        }
        return false;
    }
}
