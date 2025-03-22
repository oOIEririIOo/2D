using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Vector2 target;
    public GameObject bloodParticle;
    public Transform player;
    Transform targetTrans;
    public Transform arrowPool;
    private Rigidbody2D rb;
    public Transform user;
    public Animator arrowAnim;
    public Collider2D canAttackColl;
    private Vector2 currentUser;
    private Vector2 centerOffSet;
    public float shootAngle;
    public float tanAngle;
    public float currentAngle;
    float x=0f;
    float y=0f;
    float distence;
    float height;
    public float speedOffSet;
    private float currentSpeed;
    public float activeTime;
    public float activeTimer;
    private  float maxLifeTime;
    private Vector2 transOffSet;

    public bool isShooted;
    public bool shootPlayer;
    public float shootDir;
    float xOffSet;
    float bugSet = 0.01f;
    private void OnEnable()
    {
        this.transform.parent = arrowPool.transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bloodParticle.SetActive(false);
        bugSet = 0.05f;
        transform.position = new Vector2(user.position.x, user.position.y);
        currentUser = user.position;
        maxLifeTime = 15f;
        target = player.position;
        GetComponent<BoxCollider2D>().enabled = true;
        canAttackColl.enabled = false;
        if (currentUser.x < target.x)
        {
            target= new Vector2(target.x - 1.8f, target.y);
        }
        else target= new Vector2(target.x + 1.8f, target.y);
        rb = GetComponent<Rigidbody2D>();
        if(currentUser.x < target.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }else transform.localScale = new Vector3(-1, 1, 1);
        activeTimer = activeTime;
        distence = target.x - (currentUser.x);
        tanAngle = Mathf.Tan(shootAngle * 0.01745329252f);
        height = Mathf.Abs(distence) * tanAngle / 4f;
        currentAngle = math.degrees(math.atan(4 * height / distence - 8 * height / (distence * distence) * x));
        transform.rotation = Quaternion.AngleAxis(currentAngle, Vector3.forward);
        currentSpeed = Mathf.Abs(distence) / (Mathf.Pow((2 * height / 0.98f), 0.5f)) * speedOffSet;
        isShooted = false;
        shootPlayer = false;
        arrowAnim.SetBool("Break", false);
        arrowAnim.Play("GoblinArrow");
    }

    
    private void Update()
    {
        maxLifeTime -= Time.deltaTime;
        bugSet -= Time.deltaTime;
        if(bugSet>= 0f)
        {
            isShooted = false;
            GetComponent<BoxCollider2D>().enabled = true;
        }
        x = transform.position.x - (currentUser.x);
        y = currentUser.y + (4 * height / (distence * distence)) * x * (distence - x);
        currentAngle = math.degrees(math.atan(4 * height / distence - 8 * height / (distence * distence) * x));
        if (!isShooted)
        {
            transform.rotation = Quaternion.AngleAxis(currentAngle, Vector3.forward);
        }

       if(maxLifeTime <=0f )
        {
            ArrowPool.instance.ReturnPool(this.gameObject);
        }
        if (isShooted)
        {
            activeTimer -= Time.deltaTime;
            {
                if(activeTimer <=0)
                {
                    //·µ»Ø¶ÔÏó³Ø
                    ArrowPool.instance.ReturnPool(this.gameObject);
                }
            }
        }
        if(shootPlayer)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform.position;
            transform.position = new Vector2(target.x + (centerOffSet.x * targetTrans.transform.localScale.x * xOffSet),target.y + (centerOffSet.y));
        }
 
    }
    private void FixedUpdate()
    {
        if (!isShooted)
        {
            rb.velocity = new Vector2(currentSpeed * transform.localScale.x * Time.fixedDeltaTime, rb.velocity.y);
            transform.position = new Vector2(transform.position.x, y);
        }
    }


    public void ShootFinish()
    {
        isShooted = true;
        //rb.velocity = Vector2.zero;
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Block"))
        {
            ShootFinish();
            rb.velocity = Vector2.zero;
            centerOffSet = new Vector2(transform.position.x - target.x, transform.position.y - target.y);
            transform.position = new Vector2(target.x + (centerOffSet.x), target.y + (centerOffSet.y));
            canAttackColl.enabled = true ;
            arrowAnim.SetBool("Break", true);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 4f;
            rb.AddForce(transform.right * transform.localScale.x * (-1) * 3f, ForceMode2D.Impulse);
        }

        else if (other.CompareTag("Parry"))
        {
            ShootFinish();
            rb.velocity = Vector2.zero;
            centerOffSet = new Vector2(transform.position.x - target.x, transform.position.y - target.y);
            transform.position = new Vector2(target.x + (centerOffSet.x), target.y + (centerOffSet.y));
            canAttackColl.enabled = true;
            arrowAnim.SetBool("Break", true);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 4f;
            rb.AddForce(transform.right * transform.localScale.x *(-1) * 10f, ForceMode2D.Impulse);
        }


        if (other.CompareTag("Player"))
        {
            targetTrans = GameObject.FindGameObjectWithTag("Player").transform;
            transform.SetParent(targetTrans.transform,true);
            rb.velocity = Vector2.zero;
            ShootFinish();
            shootPlayer = true;
            target = player.position;
            bloodParticle.SetActive(true);
            centerOffSet = new Vector2(transform.position.x - target.x, transform.position.y - target.y);
            transform.position = new Vector2(target.x + (centerOffSet.x), target.y + (centerOffSet.y));
            GetComponent<BoxCollider2D>().enabled = false;
            shootDir = transform.localScale.x;
            if (targetTrans.transform.localScale.x == 1)
            {
                xOffSet = 1;
            }
            else xOffSet = -1;
        }
        
        
        if(other.CompareTag("Ground"))
        {
            ShootFinish();
            GetComponent<BoxCollider2D>().enabled = false;
            rb.velocity = Vector2.zero;
        }
    }
    
}
