using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public enum StateType
{
    Idle, Patrol, Chase, React, Attack, RangedAttack, Hurt, Dead
}


public class FSM : MonoBehaviour
{

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
    public float attackCD;
    public float attackCDTimer;
    public float rangedAtkCD;
    public float rangedAtkCDTimer;

    [Header("检测")]
    public Transform centerOffset;
    public Transform rangedOffset;
    public float checkRadius;
    public LayerMask attackLayer;
    public GameObject attackAreaAfterReact;

    protected IState currentState;
    protected IState patrolState; 
    protected IState chaseState;
    protected IState attackState;
    protected IState reactState;
    protected IState idleState;
    protected IState rangedAttackState;
    protected IState hurtState;
    protected IState deadState;

    [Header("状态")]
    public bool isHurt;
    public bool isDead;
    public bool canPatrol;
    public bool canAttack;
    public bool canRangedAtk;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        hitAnim = hitVfx.GetComponent<Animator>();
        currentSpeed = nomalSpeed;     
    }

    private void OnEnable()
    {
        currentState = idleState;
        currentState.OnEnter(this);
    }

    private void Update()
    {
        AnimCD();
        if(GetComponent<Character>().currentHealth <=0)
        {
            SwitchState(StateType.Dead);
        }
        currentState.LogicUpdate();
        if(transform.localScale.x == 1)
        {
            faceDir = 1;
        }
        else if(transform.localScale.x ==-1)
        {
            faceDir = -1;
        }
    }

    private void FixedUpdate()
    {
        currentState.PhysicsUpdate();
    }

    private void OnDisable()
    {
        currentState.OnExit();
    }

    public void SwitchState(StateType state)
    {
        var newState = state switch
        {
            StateType.Idle => idleState,
            StateType.Patrol => patrolState,
            StateType.Chase => chaseState,
            StateType.Attack => attackState,
            StateType.React => reactState, 
            StateType.RangedAttack => rangedAttackState,
            StateType.Hurt => hurtState,
            StateType.Dead => deadState,
            _ => null
        };

        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }

    public void FlipTo(Transform target)
    {
        if(target != null)
        {
            if(transform.position.x > target.position.x)
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
        if(collision.CompareTag("Player"))
        {

            target = collision.transform;
            if(target.position.x > chasePoints[0].position.x && target.position.x < chasePoints[1].position.x)
                attackAreaAfterReact?.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            target = null;
            attackAreaAfterReact?.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(centerOffset.position, checkRadius);
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
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(transform.position.x - attacker.position.x, 0).normalized;


        StartCoroutine(OnHurt(dir));
    }

    private IEnumerator OnHurt(Vector2 dir)
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        rb.velocity = new Vector2(rb.velocity.x, 6);
        yield return new WaitForSeconds(0.5f);
        isHurt = false;
    }

    public void OnDie(Transform attackTrans)
    {
        Transform attacker = attackTrans;
        if (attacker.position.x - transform.position.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (attacker.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        gameObject.layer = 2;
        anim.Play("Dead");
        isDead = true;
        
       
        

    }

    public void AnimCD()
    {
        if(canAttack == false)
        {
            attackCDTimer -= Time.deltaTime;
            if(attackCDTimer <=0)
            {
                attackCDTimer = attackCD;
                canAttack = true;
            }
        }

        if(canRangedAtk == false)
        {
            rangedAtkCDTimer -= Time.deltaTime;
            if(rangedAtkCDTimer <=0)
            {
                rangedAtkCDTimer = rangedAtkCD;
                canRangedAtk = true;
            }
        }
    }

    public void DestroyAfaterAnimation()
    {
        Destroy(this.gameObject);
    }
}
