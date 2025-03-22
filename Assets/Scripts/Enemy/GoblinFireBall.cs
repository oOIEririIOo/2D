using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinFireBall : MonoBehaviour
{
    public Transform user;
    public Vector2 user_;
    public Animator anim;
    public Rigidbody2D rb;
    public float speed;
    private float currentSpeed;
    public float activeTime;
    public float activeStart;
    public Vector2 transOffSet;
    public AudioSource fireBallLoop;
    public AudioSource fireBallHit;

    private void OnEnable()
    {
        user_ = user.position;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        transform.localScale = user.localScale;
        transform.position = new Vector2(user_.x+transOffSet.x * user.localScale.x , user_.y + transOffSet.y);
        activeStart = Time.time;
        currentSpeed = speed;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = false;
        fireBallLoop.Play();
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(currentSpeed * transform.localScale.x * Time.fixedDeltaTime, rb.velocity.y);
    }

    private void Update()
    {
        if(Time.time >= activeStart + activeTime)
        {
            //∑µªÿ∂‘œÛ≥ÿ
            StartCoroutine(AnimFinish());
        }
    }

    IEnumerator AnimFinish()
    {
        anim.SetBool("Destory", true);
        currentSpeed = speed*0.33f;
        yield return new WaitForSecondsRealtime(0.5f);
        FireBallPool.instance.ReturnPool(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!CompareTag("Parry") && !CompareTag("ParryAttack"))
        {
            if (other.CompareTag("Player") || other.CompareTag("Ground") || other.CompareTag("Enemy"))
            {
                StartCoroutine(AnimFinish());
                if(fireBallLoop.isPlaying)
                {
                    fireBallLoop.Stop();
                }
                
                fireBallHit.Play();
            }
                
        }
            
    }
}
