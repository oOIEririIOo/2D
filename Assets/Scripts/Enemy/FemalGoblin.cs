using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FemalGoblin : FSM
{
    protected FSM currentEnemy;
    public FireBallPool fireBallPool;
    protected override void Awake()
    {
        base.Awake();
        idleState = new FemalGoblinIdleState();
        patrolState = new FemalGoblinPatrolState();
        chaseState = new FemalGoblinChaseState();
        attackState = new FemalGoblinAttackState();
        rangedAttackState = new FemalGoblinRangedAttackState();
        hurtState = new FemalGoblinHurtState();
        deadState = new FemalGoblinDeadState();
    }

    public void FireBall()
    {
        fireBallPool.GetFromPool();
    }
    
}
