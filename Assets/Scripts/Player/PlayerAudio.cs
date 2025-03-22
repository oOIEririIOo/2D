using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudio : MonoBehaviour
{
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private PlayerAnimation playerAnimation;
    private PlayerController playerController;
    private Character character;

    public AudioSource stepLeft;
    public AudioSource stepRight;
    public AudioClip stepLeftClip_Grass;
    public AudioClip stepRightClip_Grass;
    public AudioClip stepLeftClip_Bridge;
    public AudioClip stepRightClip_Bridge;
    public AudioSource roll;

    public AudioSource attack;
    public AudioClip[] attackClip;
    public int attackComb = 0;
    public AudioSource airAttack;
    public AudioClip[] airAttackClip;
    public int airAttackComb = 0;

    public AudioSource mikiri;

    public AudioSource lightSlash;

    public AudioSource shield;
    public AudioSource parry;

    public AudioSource dash;
    public AudioSource holyLoop;
    public AudioSource shieldBuff;
    public AudioSource attackBuff;

    public AudioSource climbWall1;
    public AudioSource climbWall2;
    public AudioSource climbWall3;
    private void Awake()
    {
        physicsCheck = GetComponent<PhysicsCheck>();
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerController = GetComponent<PlayerController>();
        character = GetComponent<Character>();
    }
    private void Start()
    {
        stepLeft.clip = stepLeftClip_Grass;
        stepRight.clip = stepRightClip_Grass;
    }
    private void Update()
    {
        if (playerController.isAttack == false)
        {
            attackComb = 0;
        }
        if (playerController.isAirAttack == false)
        {
            airAttackComb = 0;
        }
    }




    public void StepLeft()
    {
        stepLeft.Play();
    }
    public void StepRight()
    {
        stepRight.Play();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bridge"))
        {
            stepLeft.clip = stepLeftClip_Bridge;
            stepRight.clip = stepRightClip_Bridge;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bridge"))
        {
            stepLeft.clip = stepLeftClip_Grass;
            stepRight.clip = stepRightClip_Grass;
        }
    }

    public void RollAudio()
    {
        roll.Play();
    }

    public void Attack()
    {
        
        attack.clip = attackClip[attackComb];
        attack.Play();
        attackComb = (attackComb + 1) % attackClip.Length;
    }
    public void AirAttack()
    {
        airAttack.clip = airAttackClip[airAttackComb];
        airAttack.Play();
        airAttackComb = (airAttackComb + 1) % airAttackClip.Length;
    }

    public void Mikri()
    {
        mikiri.Play();
    }

    public void LightSlashAudio()
    {
        lightSlash.Play();
    }

    public void Block1111()
    {
        shield.Play();
    }
    public void Parry111()
    {
        parry.Play();
    }

    public void DashAudio()
    {
        dash.Play();
    }
    public void HolyLoopAudioStart()
    {
        if(!holyLoop.isPlaying)
            holyLoop.Play();
    }
    public void HolyLoopAudioStop()
    {
        holyLoop.Stop();
    }

    public void ShieldBuffAudio()
    {
        shieldBuff.Play();
    }
    public void AttackBuffAudio()
    {
        attackBuff.Play();
    }

    public void ClimbWall1()
    {
        climbWall1.Play();
    }
    public void ClimbWall2()
    {
        climbWall2.Play();
    }
    public void ClimbWall3()
    {
        climbWall3.Play();
    }
}
