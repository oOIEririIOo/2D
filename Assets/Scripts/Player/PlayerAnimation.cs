using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator anim;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private PlayerController playerController;
    private PlayerSkill playerSkill;
    public GameObject runVFX;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        playerController = GetComponent<PlayerController>();
        playerSkill = GetComponent<PlayerSkill>();
    }
    private void Update()
    {
        SetAnimation();
    }
    public void SetAnimation()
    {
        anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("velocityY", rb.velocityY);
        anim.SetBool("isGround", physicsCheck.isGround);
        anim.SetBool("isDead", playerController.isDead);
        anim.SetBool("isAttack", playerController.isAttack);
        anim.SetBool("isDash", playerController.isDash);
        anim.SetBool("isAirAttack", playerController.isAirAttack);
        anim.SetBool("isMikiri", playerSkill.isMikiri);
        anim.SetBool("canMikiri", playerSkill.canMikiri);
        anim.SetBool("isLightSlash", playerSkill.isLightSlash);
        anim.SetBool("canLightSlash", playerSkill.canLightSlash);
        anim.SetBool("isPressLD", playerSkill.isPressLD);
        anim.SetBool("isBlock", playerSkill.isBlock);
        anim.SetBool("isPressBLK", playerSkill.isPressBLK);
        anim.SetBool("isAttackBuff", playerSkill.isAttackBuff);
        anim.SetBool("isShieldBuff", playerSkill.isShieldBuff);
        anim.SetBool("isHealing", playerSkill.isHealing);
        anim.SetBool("isRoll", playerSkill.isRoll);
        anim.SetBool("ClimbWallR", physicsCheck.canClimbRight && playerController.faceDir == 1);
        anim.SetBool("ClimbWallL", physicsCheck.canClimbLeft && playerController.faceDir == -1);
        anim.SetBool("isClimbWall", playerController.isClimbWall);
        anim.SetBool("RollAgain", playerSkill.rollAgain);
    }


    public void RunPartacleStart()
    {
        runVFX.SetActive(true);
    }

    public void RunPartacleStop()
    {
        runVFX.SetActive(false);
    }
    public void PlayHurt()
    {
        if (playerSkill.isBlock == false && playerSkill.isParry == false)
        {
            anim.SetTrigger("hurt");
        }
    }

    public void PlayerAttack()
    {
        anim.SetTrigger("attack");
    }
    public void AttackMove()
    {
        rb.velocity = new Vector2((float)4.5 * playerController.faceDir, rb.velocity.y);
    }
    public void Repulsed()
    {
        rb.velocity = new(-3 * playerController.faceDir, rb.velocity.y);
    }
    public void ParryFinish()
    {
        playerSkill.isSkill = false;
        anim.SetBool("isParry", false);
        playerSkill.isParry = false;
        playerSkill.isBlock = false;
    }
    public void IsSkill()
    {
        playerSkill.isSkill = true;
    }
    public void NoSkill()
    {
        playerSkill.isSkill = false;
    }
    public void VZero()
    {
        rb.velocity = Vector2.zero;
    }
    public void ClimbUp()
    {
        
        if (playerController.faceDir == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, physicsCheck.climbUpOffset + (Vector2)physicsCheck.climbPoint.position, 20f );
        }
            
        else
        {
            var climbUpOffsetL = physicsCheck.climbUpOffset;
            climbUpOffsetL.x = physicsCheck.climbUpOffset.x*(-1);
            transform.position = Vector2.MoveTowards(transform.position, (Vector2)physicsCheck.climbPoint.position + climbUpOffsetL, 100f);
        }
        
    }
           
    
}
