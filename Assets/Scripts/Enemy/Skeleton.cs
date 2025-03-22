using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using UnityEngine;

public class Skeleton : FSM
{
    protected override void Awake()
    {
        base.Awake();
        idleState = new SkeletonIdleState();
        patrolState = new SkeletonPatrolState();
        chaseState = new SkeletonChaseState();
        attackState = new SkeletonAttackState();
        reactState = new SkeletonReactState();
    }
}
