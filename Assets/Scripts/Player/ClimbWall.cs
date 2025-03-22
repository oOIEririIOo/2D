using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ClimbWall : StateMachineBehaviour
{
    Transform climbPoint_;
    public Vector2 climbRightOffset;
    public Vector2 climbLeftOffset;
    public bool climbRight;
    public bool climbLeft;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<PlayerController>().smallColl.enabled = false;
        animator.GetComponent<PlayerSkill>().isSkill = true;
        animator.GetComponent<PlayerController>().isClimbWall = true;
        animator.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        animator.GetComponent<Rigidbody2D>().gravityScale = 0f;//Ðü¿Õ
        climbPoint_ = animator.GetComponent<PhysicsCheck>().climbPoint;
        climbRightOffset = animator.GetComponent<PhysicsCheck>().climbRightOffset;
        climbLeftOffset = animator.GetComponent<PhysicsCheck>().climbLeftOffset;
        climbRight = animator.GetComponent<PhysicsCheck>().canClimbRight;
        climbLeft = animator.GetComponent<PhysicsCheck>().canClimbLeft;
        climbRightOffset = new(climbRightOffset.x - 0.33f, climbRightOffset.y);//Îü¸½µ½ÅÊÅÀµã
        climbLeftOffset = new(climbLeftOffset.x + 0.33f, climbLeftOffset.y);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (climbRight)
        {
            climbLeft = false;
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, (Vector2)climbPoint_.position - climbRightOffset, 100f);
        }
        else if (climbLeft)
        {
            climbRight = false;
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, (Vector2)climbPoint_.position - climbLeftOffset, 100f);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
