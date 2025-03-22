using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    public AudioSource stepLeft;
    public AudioSource stepRight;

    public AudioSource attack;
    public AudioClip[] attackClip;
    public int attackComb = 0;
    public AudioSource rangedAttack;

    public AudioSource skillAudio;
    public AudioClip[] skillClip;
    public int skillComb = 0;

    public AudioSource jump;

    public void StepLeft()
    {
        stepLeft.Play();
    }
    public void StepRight()
    {
        stepRight.Play();
    }
    public void Attack()
    {

        attack.clip = attackClip[attackComb];
        attack.Play();
        attackComb = (attackComb + 1) % attackClip.Length;
    }

    public void RangedAttack()
    {
        rangedAttack.Play();
    }

    public void Skill()
    {
        skillAudio.clip = skillClip[skillComb];
        skillAudio.Play();
        skillComb = (skillComb + 1) % skillClip.Length;
    }

    public void Jump()
    {
        jump.Play();
    }
}
