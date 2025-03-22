using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Parry : MonoBehaviour
{
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private PlayerAnimation playerAnimation;
    private CapsuleCollider2D coll;
    private PlayerController playerController;
    private Character character;
    private PlayerSkill playerSkill;

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

        //currentDefenseRate = character.defenseRate;
        //character.defenseRate = 0f;
    }

    private void OnDisable()
    {
        //character.defenseRate = currentDefenseRate;
    }
    public void GetAttack(Attack attacker)
    {
        //²¥·Å¶¯»­
        playerAnimation.anim.SetBool("isParry",true);
        
        playerSkill.isSkill = true;
        playerSkill.isParry = true;
        character.invulunerable = true;
        if(attacker.CompareTag("CanReBound"))
        {
            attacker.GetComponent<CircleCollider2D>().enabled = true;
            attacker.GetComponent<BoxCollider2D>().enabled = false;
            //·´µ¯
            attacker.GetComponent<Rigidbody2D>().velocity = new Vector2(attacker.GetComponent<Rigidbody2D>().velocity.x * -1, attacker.GetComponent<Rigidbody2D>().velocity.y);
            attacker.transform.localScale = new Vector3(attacker.transform.localScale.x * -1, attacker.transform.localScale.y, attacker.transform.localScale.z);
        }

    }
}
