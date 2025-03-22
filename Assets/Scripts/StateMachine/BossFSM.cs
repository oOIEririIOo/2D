using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;

using UnityEngine;


public enum BossStateType
{
    Idle, Patrol, Chase, React, Attack, Leap, SpinAttack, WalkToAttack, WalkToLeap, WalkToSpinAttack, Relax, Last, Dead, Jump,
}


public class BossFSM : MonoBehaviour
{

    public VoidEventSO buffEvent;

    public Rigidbody2D rb;
    public Animator anim;
    public PhysicsCheck physicsCheck;
    public GameObject hitVfx;
    public Animator hitAnim;

    public Transform[] patrolPoints;
    public Transform[] chasePoints;
    public Transform target;

    [Header("基本参数")]
    public float nomalSpeed;
    public float chaseSpeed;
    public float currentSpeed;
    public float hurtForce;
    public int faceDir;

    [Header("检测")]
    public Transform centerOffset;
    public float checkRadius;
    public LayerMask attackLayer;

    public BossState currentState;
    public BossState lastState;
    public BossState deadState;

    public BossState patrolState;
    public BossState chaseState;
    public BossState attackState;
    public BossState reactState;
    public BossState idleState;
    public BossState walkToAttackState;
    public BossState walkToSpinAttackState;
    public BossState walkToLeapState;
    public BossState leapState;
    public BossState spinAttackState;
    public BossState relaxState;
    public BossState jumpState;

    [Header("状态")]
    public bool isHurt;
    public bool isDead;
    public bool dashStart;

    public GameObject sentoAera;
    public GameObject healthBar;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        hitAnim = hitVfx.GetComponent<Animator>();
        currentSpeed = nomalSpeed;
    }

    protected virtual void OnEnable()
    {
        currentState = idleState;
        currentState.OnEnter(this);
    }

    protected virtual void Update()
    {
        currentState.LogicUpdate(this);
        if(GetComponent<BossCharacter>().currentHealth<=0)
        {
            SwitchState(BossStateType.Dead);
            
        }
        if(isDead)
        {
            SwitchState(BossStateType.Dead);
        }
        if (transform.localScale.x == 1)
        {
            faceDir = 1;
        }
        else if (transform.localScale.x == -1)
        {
            faceDir = -1;
        }
    }

    protected virtual void FixedUpdate()
    {
        currentState.PhysicsUpdate();
        if(dashStart)
        {
            transform.position = Vector2.Lerp(transform.position, target.transform.position, 0.05f);
        }
    }

    protected virtual void OnDisable()
    {
        currentState.OnExit();
    }

    public void SwitchState(BossStateType state)
    {
        var newState = state switch
        {
            BossStateType.Idle => idleState,
            BossStateType.Patrol => patrolState,
            BossStateType.Chase => chaseState,
            BossStateType.Attack => attackState,
            BossStateType.React => reactState,
            BossStateType.SpinAttack => spinAttackState,
            BossStateType.Leap => leapState,
            BossStateType.WalkToAttack => walkToAttackState,
            BossStateType.WalkToSpinAttack => walkToSpinAttackState,
            BossStateType.WalkToLeap => walkToLeapState,
            BossStateType.Relax => relaxState,
            BossStateType.Last => lastState,
            BossStateType.Dead => deadState,
            BossStateType.Jump => jumpState,
            _ => null
        };

        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }

    public void FlipTo(Transform target)
    {
        if (target != null)
        {
            if (transform.position.x > target.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (transform.position.x < target.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = collision.transform;
            sentoAera.SetActive(true);
            healthBar.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(centerOffset.position, checkRadius);
    }

    public void OnTakeShield()
    {
        lastState = currentState;

        SwitchState(BossStateType.Relax);

        
    }


    public void OnTakeDamage(Transform attackTrans)
    {
        Transform attacker = attackTrans;
        hitAnim.SetTrigger("Hit");
        //转身
        if (attacker.position.x - transform.position.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (attacker.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //被击退
        isHurt = true;
        anim.Play("Hit");
        /*
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
        */

       // StartCoroutine(OnHurt(dir));


    }

    private IEnumerator OnHurt(Vector2 dir)
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        rb.velocity = new Vector2(rb.velocity.x, 6);
        yield return new WaitForSeconds(0.5f);
        isHurt = false;
    }

    public void OnDie()
    {
        gameObject.layer = 2;
        anim.Play("Dead");
        isDead = true;
        
    }

    public void DashToPlayer()
    {

        dashStart = true;
        
    }

    public void DashToPlayerFinish()
    {
        dashStart = false;
    }

    public void DestroyAfaterAnimation()
    {
        Destroy(gameObject);
    }
}
