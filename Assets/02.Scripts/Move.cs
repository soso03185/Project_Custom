using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Move : Skill
{
    public GameManager gameManager;
    public GameObject target;

    public float playerSpeed;
    public float attackRange;
    private float elapsedDistance = 0;

    private const string KeyIsMove = "IsRun";

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public override bool Play(Player playerObject)
    {
        Animator animator = playerObject.GetComponent<Animator>();
        animator.SetBool(KeyIsMove, true);


        if(target == null)
        {
            target = gameManager.GetNearestMonster();
        }

        if (target == null)
        {
            return false;
        }
        Vector3 direction = (target.transform.position - playerObject.transform.position).normalized;

        direction.y = 0;

        playerObject.transform.rotation = Quaternion.LookRotation(direction);

        playerObject.transform.position += direction * playerSpeed * Time.deltaTime;

        elapsedDistance = Vector3.Distance(target.transform.position, playerObject.transform.position);

        //elapsedDistance += Time.deltaTime;
        //playerObject.transform.Translate(Vector3.forward * Time.deltaTime);
        if (elapsedDistance <= attackRange)
        {
            animator.SetBool(KeyIsMove, false);
            return true;
        }
        return false;
    }
}
