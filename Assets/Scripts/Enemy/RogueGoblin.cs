using NUnit.Framework.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueGoblin : FSM
{
    protected FSM currentEnemy;
    public ArrowPool arrowPool;
    protected override void Awake()
    {
        base.Awake();
        idleState = new RogueGoblinIdleState();
        patrolState = new RougeGoblinPatrolState();
        chaseState = new RogueGoblinChaseState();
        attackState = new RogueGoblinAttackState();
        rangedAttackState = new RogueGoblinRangedAttackState();
        hurtState = new RogueGoblinHurtState();
        deadState = new RogueGoblinDeadState();
    }

    public void TakeArrow()
    {
        arrowPool.GetFromPool();
    }

    public void Attack3()
    {
        rb.AddForce(transform.right * transform.localScale.x * 20f, ForceMode2D.Impulse);
    }

    public void Stop()
    {
        rb.velocity = Vector2.zero;
    }
}
