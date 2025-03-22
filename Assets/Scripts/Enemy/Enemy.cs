using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public PhysicsCheck physicsCheck;

    [Header("基本参数")]
    public float nomalSpeed;
    public float chaseSpeed;
    public float currentSpeed;
    public Vector3 faceDir;
    public float hurtForce;

    [Header("检测")]
    public Vector2 centerOffset;
    public Vector2 checkSize;
    public float checkDistance;
    public LayerMask attackLayer;

    public Transform attacker;
    [Header("计时器")]
    public float waitTime;
    public float waitTimeCounter;
    public bool wait;
    public float lostTime;
    public float lostTimeCounter;

    [Header("状态")]
    public bool isHurt;
    public bool isDead;

    protected BaseState patrolState;
    protected BaseState currentState;
    protected BaseState chaseState;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        currentSpeed = nomalSpeed;
        waitTimeCounter = waitTime;
    }

    private void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter(this);
    }

    private void Update()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
        currentState.LogicUpdate();
        TimeCounter();
    }
    private void FixedUpdate()
    {
        if(isHurt == false && isDead == false && wait == false)
        {
            Move();
        }

        currentState.PhysicsUpdate();
    }

    private void OnDisable()
    {
        currentState.OnExit();
    }
    public virtual void Move()
    {
        rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);
    }

    public void TimeCounter()
    {
        if(wait == true)
        {
            waitTimeCounter -= Time.deltaTime;
            if(waitTimeCounter <=0)
            {
                wait = false;
                waitTimeCounter = waitTime;
                transform.localScale = new Vector3(faceDir.x, 1, 1);
                physicsCheck.bottom0ffset.x *= -1;
                anim.SetBool("walk", true);
            }
        }

        if(! FoundPlayer() && lostTimeCounter>0)
        {
            lostTimeCounter -= Time.deltaTime;
        }
    }

    #region 事件执行方法
    public void OnTakeDamage(Transform attackTrans)
    {
        attacker = attackTrans;      
        //转身
        if(attacker.position.x - transform.position.x>0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (attacker.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        //被击退
        isHurt = true;
        anim.SetTrigger("hurt");
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(transform.position.x - attacker.position.x,0).normalized;


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

    public void OnDie()
    {
        gameObject.layer = 2;
        anim.SetBool("dead", true);
        isDead = true;
    }

    public bool FoundPlayer()
    {
        return Physics2D.BoxCast(transform.position + (Vector3)centerOffset, checkSize, 0, faceDir, checkDistance, attackLayer);
    }

    public void DestroyAfaterAnimation()
    {
        Destroy(this.gameObject);
    }

    public void SwitchState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.Patrol => patrolState,
            NPCState.Chase => chaseState,
            _ => null
        };

        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }

    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffset + new Vector3(checkDistance*-transform.localScale.x,0,0), 0.2f);
    }
}
