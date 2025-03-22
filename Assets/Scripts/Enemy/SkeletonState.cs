using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonIdleState : IState

{

    private float IdleTime = 0f;
    public override void OnEnter(FSM enemy)
    {
        currentEnemy = enemy;
        enemy.anim.Play("Idle");
    }
    public override void LogicUpdate()
    {
        IdleTime += Time.deltaTime;
        if(currentEnemy.target != null && currentEnemy.transform.position.x > currentEnemy.chasePoints[0].position.x && currentEnemy.transform.position.x < currentEnemy.chasePoints[1].position.x)
        {
            currentEnemy.SwitchState(StateType.React);
        }
        if (IdleTime >= 1.35f)
        {
            currentEnemy.SwitchState(StateType.Patrol);
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


public class SkeletonPatrolState : IState
{

    private int patrolPointNum;
    public override void OnEnter(FSM enemy)
    {
        currentEnemy = enemy;
        currentEnemy.anim.Play("Walk");
        
    }
    public override void LogicUpdate()
    {
        if (currentEnemy.target != null && currentEnemy.transform.position.x > currentEnemy.chasePoints[0].position.x && currentEnemy.transform.position.x < currentEnemy.chasePoints[1].position.x)
        {
            currentEnemy.SwitchState(StateType.React);
        }
    }

    public override void PhysicsUpdate()
    {
        currentEnemy.transform.position = Vector2.MoveTowards(currentEnemy.transform.position, currentEnemy.patrolPoints[patrolPointNum].position, currentEnemy.currentSpeed *(float)0.01 * Time.deltaTime);
        currentEnemy.FlipTo(currentEnemy.patrolPoints[patrolPointNum]);
        if (Mathf.Abs(Vector2.Distance(currentEnemy.transform.position, currentEnemy.patrolPoints[patrolPointNum].position)) < 1f)
        {
            currentEnemy.SwitchState(StateType.Idle);
        }

    }

    public override void OnExit()
    {
        patrolPointNum = (patrolPointNum+1)%currentEnemy.patrolPoints.Length;

    }

}


public class SkeletonChaseState : IState
{
    public override void OnEnter(FSM enemy)
    {
        
        currentEnemy = enemy;
        currentEnemy.anim.Play("Walk");
    }
    public override void LogicUpdate()
    {
        currentEnemy.FlipTo(currentEnemy.target);
         if (currentEnemy.target == null || currentEnemy.target.transform.position.x < currentEnemy.chasePoints[0].position.x || currentEnemy.transform.position.x > currentEnemy.chasePoints[1].position.x)
        {       
            currentEnemy.SwitchState(StateType.Idle);
        }
        if (Physics2D.OverlapCircle(currentEnemy.centerOffset.position, currentEnemy.checkRadius,currentEnemy.attackLayer))
        {
            currentEnemy.SwitchState(StateType.Attack);
        }
    }

    public override void PhysicsUpdate()
    {
        if (currentEnemy.target)
        {
            currentEnemy.transform.position = Vector2.MoveTowards(currentEnemy.transform.position, currentEnemy.target.position, currentEnemy.chaseSpeed*(float)0.01 * Time.deltaTime);
        }
    }

    public override void OnExit()
    {
        
    }

}



public class SkeletonAttackState : IState
{
    private AnimatorStateInfo info;
    public override void OnEnter(FSM enemy)
    {
        currentEnemy = enemy;
        currentEnemy.anim.Play("Attack");
    }
    public override void LogicUpdate()
    {
        info = currentEnemy.anim.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 0.95f)
        {
            currentEnemy.SwitchState(StateType.Chase);
        }
    }

    public override void PhysicsUpdate()
    {
        
    }

    public override void OnExit()
    {
        
    }

}


public class SkeletonReactState : IState
{

    private AnimatorStateInfo info;
    public override void OnEnter(FSM enemy)
    {
        currentEnemy = enemy;
        currentEnemy.anim.Play("React");
    }
    public override void LogicUpdate()
    {
        info = currentEnemy.anim.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 0.95f)
        {
            currentEnemy.SwitchState(StateType.Chase);
        }
    }

    public override void PhysicsUpdate()
    {
        
    }

    public override void OnExit()
    {

    }

}

