using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UncleIdleState : BossState
{
    private float IdleTime = 0f;
    public override void OnEnter(BossFSM enemy)
    {
        currentEnemy = enemy;
        enemy.anim.Play("Idle");
    }
    public override void LogicUpdate(BossFSM enemy)
    {
        currentEnemy.FlipTo(currentEnemy.target);
        IdleTime += Time.deltaTime;
        if(IdleTime >= 2f)
        {
            currentEnemy.SwitchState(BossStateType.WalkToAttack);
        }
    }

    public override void PhysicsUpdate()
    {
        
    }

    public override void OnExit()
    {
        IdleTime = 0f;
    }

}

public class UncleWalkToAttackState : BossState
{
    
    public override void OnEnter(BossFSM enemy)
    {
        currentEnemy = enemy;
        currentEnemy.anim.Play("Walk");

    }
    public override void LogicUpdate(BossFSM enemy)
    {
        currentEnemy.FlipTo(currentEnemy.target);
        if (Physics2D.OverlapCircle(currentEnemy.centerOffset.position, currentEnemy.checkRadius, currentEnemy.attackLayer))
        {
            currentEnemy.SwitchState(BossStateType.Attack);
        }
    }

    public override void PhysicsUpdate()
    {
        if (currentEnemy.target)
        {
            currentEnemy.transform.position = Vector2.MoveTowards(currentEnemy.transform.position, currentEnemy.target.position, currentEnemy.chaseSpeed * (float)0.01 * Time.deltaTime);
        }
    }

    public override void OnExit()
    {
        
    }

}

public class UncleAttackState : BossState
{
    private AnimatorStateInfo info;
    public override void OnEnter(BossFSM enemy)
    {
        currentEnemy = enemy;
        currentEnemy.anim.Play("Attack");
    }
    public override void LogicUpdate(BossFSM enemy)
    {
        info = currentEnemy.anim.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 0.95f)
        {
            currentEnemy.SwitchState(BossStateType.WalkToSpinAttack);
        }
    }

    public override void PhysicsUpdate()
    {
        
    }

    public override void OnExit()
    {
        
    }

}



public class UncleWalkToSpinAttackState : BossState
{

    public override void OnEnter(BossFSM enemy)
    {
        currentEnemy = enemy;
        currentEnemy.anim.Play("Walk");

    }
    public override void LogicUpdate(BossFSM enemy)
    {
        currentEnemy.FlipTo(currentEnemy.target);
        if (Physics2D.OverlapCircle(currentEnemy.centerOffset.position, currentEnemy.checkRadius, currentEnemy.attackLayer))
        {
            currentEnemy.SwitchState(BossStateType.SpinAttack);
        }
    }

    public override void PhysicsUpdate()
    {
        if (currentEnemy.target)
        {
            currentEnemy.transform.position = Vector2.MoveTowards(currentEnemy.transform.position, currentEnemy.target.position, currentEnemy.chaseSpeed * (float)0.01 * Time.deltaTime);
        }
    }

    public override void OnExit()
    {

    }

}

public class UncleSpinAttackState : BossState
{
    private AnimatorStateInfo info;
    public override void OnEnter(BossFSM enemy)
    {
        currentEnemy = enemy;
        currentEnemy.anim.Play("SpinAttack");
    }
    public override void LogicUpdate(BossFSM enemy)
    {
        info = currentEnemy.anim.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 0.95f)
        {
            currentEnemy.SwitchState(BossStateType.WalkToLeap);
        }
    }

    public override void PhysicsUpdate()
    {

    }

    public override void OnExit()
    {

    }

}



public class UncleWalkToLeapState : BossState
{

    public override void OnEnter(BossFSM enemy)
    {       
        currentEnemy = enemy;
        currentEnemy.anim.Play("Walk");

    }
    public override void LogicUpdate(BossFSM enemy)
    {
        currentEnemy.FlipTo(currentEnemy.target);
        if (Physics2D.OverlapCircle(currentEnemy.centerOffset.position, currentEnemy.checkRadius, currentEnemy.attackLayer))
        {
            currentEnemy.SwitchState(BossStateType.Leap);
        }
    }

    public override void PhysicsUpdate()
    {
        if (currentEnemy.target)
        {
            currentEnemy.transform.position = Vector2.MoveTowards(currentEnemy.transform.position, currentEnemy.target.position, currentEnemy.chaseSpeed * (float)0.01 * Time.deltaTime);
        }
    }

    public override void OnExit()
    {

    }

}

public class UncleLeapState : BossState
{
    private AnimatorStateInfo info;
    public override void OnEnter(BossFSM enemy)
    {
       
        currentEnemy = enemy;
        currentEnemy.anim.Play("Leap");
    }
    public override void LogicUpdate(BossFSM enemy)
    {
        info = currentEnemy.anim.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 0.95f)
        {
            currentEnemy.SwitchState(BossStateType.WalkToAttack);
        }
        
    }

    public override void PhysicsUpdate()
    {

    }

    public override void OnExit()
    {

    }

}


//力竭状态：记录上一状态，力竭结束后返回上一状态


public class UncleRelaxState : BossState
{
    private AnimatorStateInfo info;
    public override void OnEnter(BossFSM enemy)
    {
        currentEnemy = enemy;
        currentEnemy.anim.Play("Relax");
    }
    public override void LogicUpdate(BossFSM enemy)
    {
        info = currentEnemy.anim.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 0.95f)
        {
            currentEnemy.SwitchState(BossStateType.Last);
        }
    }

    public override void PhysicsUpdate()
    {
        
    }

    public override void OnExit()
    {
        
    }

}

public class UncleDeadState : BossState
{
    public override void OnEnter(BossFSM enemy)
    {
        currentEnemy = enemy;
        currentEnemy.anim.Play("Dead");
    }
    public override void LogicUpdate(BossFSM enemy)
    {
       
    }

    public override void PhysicsUpdate()
    {
        
    }

    public override void OnExit()
    {
        
    }

}


public class UncleJumpState : BossState
{
    private AnimatorStateInfo info;
    public override void OnEnter(BossFSM enemy)
    {
        Debug.Log("1111");
        currentEnemy = enemy;
        currentEnemy.anim.Play("Jump");
    }
    public override void LogicUpdate(BossFSM enemy)
    {
        
    }

    public override void PhysicsUpdate()
    {
        info = currentEnemy.anim.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 0.9f)
        {
            currentEnemy.SwitchState(BossStateType.WalkToAttack);
        }
    }

    public override void OnExit()
    {

    }

}

