using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using UnityEngine;
using UnityEngine.Events;

public class Uncle : BossFSM
{
    private CapsuleCollider2D coll;
    


    protected override void Awake()
    {
        base.Awake();
        coll = GetComponent<CapsuleCollider2D>();
        idleState = new UncleIdleState();
        walkToAttackState = new UncleWalkToAttackState();
        attackState = new UncleAttackState();
        walkToSpinAttackState = new UncleWalkToSpinAttackState();
        spinAttackState = new UncleSpinAttackState();
        walkToLeapState = new UncleWalkToLeapState();
        leapState = new UncleLeapState();
        relaxState = new UncleRelaxState();
        deadState = new UncleDeadState();
        jumpState = new UncleJumpState();

        
    }


    protected override void OnEnable()
    {
        base.OnEnable();
        buffEvent.OnEventRaised += OnBuffEvent;
    }



    protected override void OnDisable()
    {
        base.OnDisable();
        buffEvent.OnEventRaised -= OnBuffEvent;
    }
    
    private void OnBuffEvent()
    {
        if(currentState == walkToAttackState || currentState == walkToSpinAttackState || currentState == walkToLeapState)
        SwitchState(BossStateType.Jump);
    }
    
    
    public void SpinAttackAnimation()
    {
        
        rb.velocity = new Vector2(faceDir * 120 * Time.fixedDeltaTime, rb.velocity.y);

    }

    public void SpinAttackFinish()
    {
        
        rb.velocity = Vector2.zero;
    }

    public void LeapStart()
    {    
        coll.enabled = false;
    }

    public void LeapFinish()
    {
        coll.enabled = true;
    }

    public void HitFinish()
    {
        isHurt = false;
    }
}


