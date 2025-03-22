using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using System.Xml;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;

public class FighterGoblin : FSM
{
    protected FSM currentEnemy;
    public UnityEvent cemaraShakeEvent;
    protected override void Awake()
    {
        base.Awake();
        idleState = new FighterGoblinIdleState();
        patrolState = new FighterGoblinPatrolState();
        chaseState = new FighterGoblinChaseState();
        attackState = new FighterGoblinAttackState();
        hurtState = new FighterGoblinHurtState();
        deadState = new FighterGoblinDeadState();
    }

    public void CameraShake()
    {
        if(Mathf.Abs(GameObject.FindGameObjectWithTag("Player").transform.position.x - transform.position.x) <= 24f)
        {
            cemaraShakeEvent?.Invoke();
        }

    }

    public void Attack3()
    {
        rb.AddForce(transform.right * transform.localScale.x * 8f, ForceMode2D.Impulse);
    }

    public void Stop()
    {
        rb.velocity = Vector2.zero;
    }
}


