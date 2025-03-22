using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public Transform climbPoint;

    [Header("检测参数")]
    public Vector2 bottom0ffset;
    public Vector2 left0ffset;
    public Vector2 right0ffset;
    public Vector2 up0ffset;
    public Vector2 climbRightOffset;
    public Vector2 climbLeftOffset;
    public Vector2 climbUpOffset;

    public float checkRaduis;
    public LayerMask groundLayer;
    public LayerMask climbLayer;
    public PlayerController playerController;

    [Header("状态")]
    public bool isGround;
    public bool touchLeftWall;
    public bool touchRightWall;
    public bool touchUpWall;
    public bool canClimbRight;
    public bool canClimbLeft;



    private void Update()
    {
        Check();
    }

    private void FixedUpdate()
    {
        Check();
    }
    public void Check()
    {
        //检测地面
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottom0ffset, checkRaduis, groundLayer);

        //墙体判断
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + left0ffset, checkRaduis, groundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + right0ffset, checkRaduis, groundLayer);
        touchUpWall = Physics2D.OverlapCircle((Vector2)transform.position + up0ffset, checkRaduis, groundLayer);

        //可攀爬点
        canClimbRight = (Physics2D.OverlapCircle((Vector2)transform.position + climbRightOffset, checkRaduis*2.2f, climbLayer));
        canClimbLeft = (Physics2D.OverlapCircle((Vector2)transform.position + climbLeftOffset, checkRaduis*2.2f, climbLayer));

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Climb"))
        {
            climbPoint = collision.transform;  
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Climb"))
        {
            climbPoint = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottom0ffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + left0ffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + right0ffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + up0ffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + climbRightOffset, checkRaduis*2.2f);
        Gizmos.DrawWireSphere((Vector2)transform.position + climbLeftOffset, checkRaduis*2.2f);
    }
}
