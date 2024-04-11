using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Move_JaeHyeon : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Animator animator;

    private const string key_isRun = "IsRun";
    private const string key_isAttack01 = "IsAttack01";
    private const string key_isAttack02 = "IsAttack02";
    private const string key_isJump = "IsJump";
    private const string key_isDamage = "IsDamage";
    private const string key_isDead = "IsDead";

    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    void Update()
    {
        OnKeyboard();
        RayCastHit();
    }
    
    void OnKeyboard()
    {

        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            transform.position += Vector3.forward * Time.deltaTime * _speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            transform.position += Vector3.back * Time.deltaTime * _speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }

        // Move
        if (Input.GetKey(KeyCode.UpArrow) || (Input.GetKey(KeyCode.DownArrow)) || (Input.GetKey(KeyCode.LeftArrow)) || (Input.GetKey(KeyCode.RightArrow)))
        {
            this.animator.SetBool(key_isRun, true);
        }
        else
        {
            this.animator.SetBool(key_isRun, false);
        }

        // Attack01
        if (Input.GetKeyUp("z"))
        {
            this.animator.SetBool(key_isAttack01, true);
        }
        else
        {
            this.animator.SetBool(key_isAttack01, false);
        }

        // Attack02
        if (Input.GetKeyUp("x"))
        {
            this.animator.SetBool(key_isAttack02, true);
        }
        else
        {
            this.animator.SetBool(key_isAttack02, false);
        }

        // Jump
        if (Input.GetKeyUp("space"))
        {
            this.animator.SetBool(key_isJump, true);
        }
        else
        {
            this.animator.SetBool(key_isJump, false);
        }

        // Damaged
        if (Input.GetKeyUp("c"))
        {
            this.animator.SetBool(key_isDamage, true);
        }
        else
        {
            this.animator.SetBool(key_isDamage, false);
        }
    }

    private void RayCastHit()
    {
        
    }
}

