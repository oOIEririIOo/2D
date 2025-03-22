using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage;
    public float attackRange;
    public float attackRate;

    public CharactEventSO BuffEvent;
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<Character>()?.TakeDamage(this);
        other.GetComponent<BossCharacter>()?.TakeDamage(this);
        other.GetComponent<MikiriDodge>()?.GetAttack(this);
        other.GetComponent<Block>()?.GetAttack(this);
        other.GetComponent<Parry>()?.GetAttack(this);
        other.GetComponent<TraningDummy>()?.TakeHit(this);
    }

    private void OnEnable()
    {

        BuffEvent.onEventRaised += OnBuffEvent;
    }

    

    private void OnDisable()
    {
        BuffEvent.onEventRaised -= OnBuffEvent;
        attackRate = 1f;
        
    }

    private void OnBuffEvent(Character character)
    {
        attackRate = character.attackRate;
    }

}
