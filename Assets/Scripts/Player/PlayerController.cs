using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private PlayerAnimation playerAnimation;
    public CapsuleCollider2D coll;
    public BoxCollider2D smallColl;
    private PlayerSkill playerSkill;
    private Character character;
    public Transform playerTransform;
    public GameObject DashSmoke;
    public GameObject JumpSmoke1;
    public GameObject JumpSmoke2;

    public float inputDirection;
    [Header("基本参数")]
    public float speed;
    public float jumpForce;
    public float hurtForce;
    public int jumpCnt = 2;
    private double dashForceRight = 40;
    private double dashForceLeft = 40; 

    [Header("状态")]
    public bool isHurt;
    public bool isDead;
    public bool isAttack;
    public bool isDash;
    public bool isAirAttack;
    public int faceDir;
    public bool canAirAttack;
    public bool canDash;
    public bool isClimbWall;

    [Header("物理材质")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;


    [Header("事件")]
    public UnityEvent DashEvent;
    private void Awake()
    {
        inputControl = new PlayerInputControl();
        physicsCheck = GetComponent<PhysicsCheck>();
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerSkill = GetComponent<PlayerSkill>();
        character = GetComponent<Character>();
        inputControl.GamePlay.Jump.started += Jump;
        inputControl.GamePlay.Attack.started += PlayerAttack;
        inputControl.GamePlay.Dash.started += Dash;

        canDash = true;
    }

    

    private void OnEnable()
    {
        inputControl.Enable();
    }
    private void OnDisable()
    {
        inputControl.Disable();
    }
    private void Update()
    {
        inputDirection = Input.GetAxis("Horizontal");    
        ChckState();
        if (physicsCheck.isGround)
        {
            jumpCnt = 2;
            canAirAttack = true;
        }
        

        if(isAirAttack)
        {
            rb.velocity = new Vector2(rb.velocity.x * 0.05f * Time.deltaTime, rb.velocity.y * 0.05f * Time.deltaTime);
        }

        if(isDash)
        {
            isAttack = false;
            if(faceDir == 1 )
            {
                rb.velocity = new Vector2((float)(dashForceRight -= 80 * Time.deltaTime), rb.velocity.y);

            }
            else if(faceDir == -1)
            {
                
                rb.velocity = new Vector2((float)(dashForceLeft += 80 * Time.deltaTime), rb.velocity.y);

            }
            if((rb.velocity.x <= 0 && faceDir ==1)||(rb.velocity.x >=0 && faceDir ==-1))
            {
                isDash = false;
            }
        }
     
    }
    private void FixedUpdate()
    {
        if( ! isHurt && ! isDead && ! isAttack && ! isDash && ! isAirAttack && ! playerSkill.isSkill && !isClimbWall)
        {
            Move();
        }
        
    }



    public void Move()
    {
        rb.velocity = new Vector2(inputDirection * speed * Time.deltaTime,rb.velocity.y);
        //人物翻转
        faceDir = (int)transform.localScale.x;
        if(inputDirection > 0)
        {
            faceDir = 1;
        }
        else if(inputDirection < 0)
        {
            faceDir = -1;
        }
        transform.localScale = new Vector3(faceDir, 1, 1);
    }




    private void Jump(InputAction.CallbackContext context)
    {
        if(isClimbWall)
            playerAnimation.anim.SetTrigger("ClimbWall");
        if(!isAttack && !playerSkill.isSkill &&!isDash &&!isClimbWall)
        {
            if (physicsCheck.isGround || jumpCnt > 1.1f)
            {
                canAirAttack = true;
                if (physicsCheck.isGround == true)
                {
                    JumpSmoke1.SetActive(true);
                }
                if (physicsCheck.isGround == false)
                {
                    JumpSmoke2.SetActive(true);
                }
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                jumpCnt--;
            }
        }
        

        
        
    }

    private void PlayerAttack(InputAction.CallbackContext context)
    {
        if(!isDash && !playerSkill.isSkill && !isClimbWall)
        {
            rb.velocity = Vector2.zero;
            playerAnimation.PlayerAttack();
            if (physicsCheck.isGround)
            {
                isAttack = true;
            }
            else if (physicsCheck.isGround == false && canAirAttack)
            {
                isAirAttack = true;
            }
        }
        

    }

public void GetHurt(Transform attacker)
    {
        if (playerSkill.isBlock == false && !playerSkill.isParry)
        {
            isHurt = true;
            isAttack = false;
            rb.velocity = Vector2.zero;
            Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;

            rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
            //rb.AddForce(transform.up * 8, ForceMode2D.Impulse);
        }
    }
    
    public void PlayerDead()
    {
        isDead = true;
        rb.velocity = Vector2.zero;
        inputControl.GamePlay.Disable();
        
    }
    private void ChckState()
    {
        coll.sharedMaterial = physicsCheck.isGround ? normal : wall;
        smallColl.sharedMaterial = physicsCheck.isGround ? normal : wall;
    }

    public void AirAttackFinish()
    {
        isAirAttack = false;
    }

    //Skill


    //Dash
    private void Dash(InputAction.CallbackContext context)
    {
        
        if (isDash == false && physicsCheck.isGround && canDash && !playerSkill.isSkill && !isHurt)
        {
            playerSkill.isSkill = true;
            playerAnimation.anim.SetTrigger("Dash");
            rb.gravityScale = 0.1f;
            DashEvent?.Invoke();
            DashSmoke.SetActive(true);
            isDash = true;
            character.skillInvulunerable = true;
            dashForceRight = 40;
            dashForceLeft = -40;
            rb.velocity = new Vector2((float)dashForceRight * faceDir, rb.velocity.y);
        }
        
    }

   


}
