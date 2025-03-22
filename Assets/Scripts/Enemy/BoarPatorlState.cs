using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarPatorlState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.nomalSpeed;
        currentEnemy.anim.SetBool("walk", true);

    }
    public override void LogicUpdate()
    {
        if(currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(NPCState.Chase);
        }

        if (currentEnemy.physicsCheck.isGround == false ||(currentEnemy.physicsCheck.touchLeftWall && currentEnemy.faceDir.x < 0) || (currentEnemy.physicsCheck.touchRightWall && currentEnemy.faceDir.x > 0))
        {
            currentEnemy.wait = true;
            currentEnemy.anim.SetBool("walk", false);
        }
    }


    public override void PhysicsUpdate()
    {
     
    }
    public override void OnExit()
    {
        currentEnemy.anim.SetBool("walk", false);
    }

    
}
