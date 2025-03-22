using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BoarChaseState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.anim.SetBool("run", true);
    }

    public override void LogicUpdate()
    {
        if(currentEnemy.lostTimeCounter <=0)
        {
            currentEnemy.SwitchState(NPCState.Patrol);
        }
        if (currentEnemy.physicsCheck.isGround == false || (currentEnemy.physicsCheck.touchLeftWall && currentEnemy.faceDir.x < 0) || (currentEnemy.physicsCheck.touchRightWall && currentEnemy.faceDir.x > 0))
        {
            currentEnemy.transform.localScale = new Vector3(currentEnemy.faceDir.x, 1, 1);
            currentEnemy.physicsCheck.bottom0ffset.x *= -1;
        }
    }
  
    public override void PhysicsUpdate()
    {
        
    }

    public override void OnExit()
    {
        currentEnemy.lostTimeCounter = currentEnemy.lostTime;
        currentEnemy.anim.SetBool("run", false);
    }
}
