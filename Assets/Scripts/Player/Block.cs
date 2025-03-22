using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Block : MonoBehaviour
{
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private PlayerAnimation playerAnimation;
    private CapsuleCollider2D coll;
    private PlayerController playerController;
    private Character character;
    private PlayerSkill playerSkill;
    public int hitCount = 0;

    private float currentDefenseRate;

    private void Awake()
    {       
        physicsCheck = GetComponent<PhysicsCheck>();
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        coll = GetComponent<CapsuleCollider2D>();
        playerController = GetComponent<PlayerController>();
        character = GetComponent<Character>();
    }
        private void OnEnable()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerAnimation = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnimation>();
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        playerSkill = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkill>();
        currentDefenseRate = character.defenseRate;
        //character.defenseRate = 0f;
        hitCount = 0;
    }

    private void Update()
    {
        if(hitCount > 2)
        {
            playerAnimation.anim.Play("Block3");
            playerSkill.isBlock = false;
            playerSkill.isPressBLK = false;
            playerAnimation.anim.ResetTrigger("getAttack");
            hitCount = 0;
        }
    }

    private void OnDisable()
    {
        character.defenseRate = currentDefenseRate;
    }
    public void GetAttack(Attack attacker)
    {
        //²¥·Å¶¯»­
        playerAnimation.anim.SetTrigger("getAttack");
        hitCount++;
    }
    
}
