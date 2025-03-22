using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterGoblinIdleState : IState
{
    private float IdleTime = 0f;
    public override void OnEnter(FSM enemy)
    {
        enemy.anim.SetBool("run", false);
        currentEnemy = enemy;
        enemy.anim.Play("idle");
    }
    public override void LogicUpdate()
    {
        IdleTime += Time.deltaTime;
        if (IdleTime >= 1.35f && currentEnemy.canPatrol == true)
        {
            currentEnemy.SwitchState(StateType.Patrol);
        }
        else if (IdleTime >= 1.35f && currentEnemy.target != null)
        {
            currentEnemy.SwitchState(StateType.Chase);
            currentEnemy.canPatrol = true;
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


public class FighterGoblinPatrolState : IState
{
    private int patrolPointNum;
    public override void OnEnter(FSM enemy)
    {
        currentEnemy = enemy;
        enemy.anim.SetBool("run", true);

    }
    public override void LogicUpdate()
    {
        if (currentEnemy.target != null && currentEnemy.target.position.x > currentEnemy.chasePoints[0].position.x && currentEnemy.target.position.x < currentEnemy.chasePoints[1].position.x)
        {
            currentEnemy.SwitchState(StateType.Chase);
        }
    }

    public override void PhysicsUpdate()
    {
        Vector2 target_x = new Vector2(currentEnemy.patrolPoints[patrolPointNum].position.x, currentEnemy.transform.position.y);
        currentEnemy.transform.position = Vector2.MoveTowards(currentEnemy.transform.position, target_x, currentEnemy.currentSpeed * (float)0.01 * Time.fixedDeltaTime);
        currentEnemy.FlipTo(currentEnemy.patrolPoints[patrolPointNum]);
        if (Mathf.Abs(Vector2.Distance(currentEnemy.transform.position, target_x)) < 0.2f)
        {
            currentEnemy.anim.SetBool("run", false);
            //currentEnemy.SwitchState(StateType.Idle); 在动画事件中更改状态
        }
    }

    public override void OnExit()
    {
        patrolPointNum = (patrolPointNum + 1) % currentEnemy.patrolPoints.Length;
    }


}

public class FighterGoblinChaseState : IState
{

    public override void OnEnter(FSM enemy)
    {

        currentEnemy = enemy;
        currentEnemy.anim.Play("run1");
        enemy.anim.SetBool("run", true);

    }
    public override void LogicUpdate()
    {
        currentEnemy.FlipTo(currentEnemy.target);

        if (currentEnemy.target == null || currentEnemy.target.transform.position.x < currentEnemy.chasePoints[0].position.x || currentEnemy.target.transform.position.x > currentEnemy.chasePoints[1].position.x)
        {

            currentEnemy.SwitchState(StateType.Idle);
        }
        
        else if (currentEnemy.canAttack == true && Physics2D.OverlapCircle(currentEnemy.centerOffset.position, currentEnemy.checkRadius, currentEnemy.attackLayer))
        {
            currentEnemy.SwitchState(StateType.Attack);
        }
    }

    public override void PhysicsUpdate()
    {
        if (currentEnemy.target)
        {
            Vector2 target_x = new Vector2(currentEnemy.target.position.x, currentEnemy.transform.position.y);
            currentEnemy.transform.position = Vector2.MoveTowards(currentEnemy.transform.position, target_x, currentEnemy.chaseSpeed * (float)0.01 * Time.deltaTime);
        }
    }

    public override void OnExit()
    {

    }

}

public class FighterGoblinAttackState : IState
{
    public override void OnEnter(FSM enemy)
    {

        enemy.anim.SetBool("run", false);
        currentEnemy = enemy;
        currentEnemy.anim.Play("attack");
    }
    public override void LogicUpdate()
    {
        //在动画事件中退出
    }

    public override void PhysicsUpdate()
    {
        //currentEnemy.rb.velocity = Vector2.zero;
    }


    public override void OnExit()
    {
        currentEnemy.canAttack = false;
    }
}

public class FighterGoblinHurtState : IState
{
    float hurtTime;
    public override void OnEnter(FSM enemy)
    {
        hurtTime = 0f;
        enemy.anim.SetBool("run", false);
        currentEnemy = enemy;
    }
    public override void LogicUpdate()
    {
        hurtTime += Time.deltaTime;
        if (hurtTime >= 0.5f)
        {
            currentEnemy.SwitchState(StateType.Chase);
        }
    }

    public override void PhysicsUpdate()
    {

    }


    public override void OnExit()
    {
        hurtTime = 0f;
    }
}

public class FighterGoblinDeadState : IState
{
    public override void OnEnter(FSM enemy)
    {
        enemy.anim.SetBool("run", false);
        currentEnemy = enemy;
        currentEnemy.anim.Play("Dead");
    }
    public override void LogicUpdate()
    {

    }

    public override void PhysicsUpdate()
    {

    }


    public override void OnExit()
    {

    }
}
