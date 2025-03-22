using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerSkill : MonoBehaviour
{
    public PlayerInputControl inputControl;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private PlayerAnimation playerAnimation;
    private PlayerController playerController;
    private Character character;
    public GameObject mikiriDodge;
    public GameObject mikiriSomke;
    public UnityEvent playerBehavior;

    [Header("技能状态")]
    public bool isSkill;
    

    public bool isMikiri;
    public bool canMikiri;
    public float mikiriForce;
    private float mikiriForceRight;

    public bool isPressLD;
    public bool isLightSlash;
    public float lightSlashTime;
    public float lightSlashStartTime;
    public bool canLightSlash;
    public float lightSlashForce;

    public bool isPressBLK;
    public bool isBlock;
    public bool isParry;
    public bool isRoll;
    public bool rollAgain;
    [Header("Buff状态")]

    public bool getBuff;
    public bool isAttackBuff;
    public Attack attackArea;

    public bool isShieldBuff;  
    public bool attackBuff;
    public bool shieldBuff;
    public float attackBuffTime;
    public float shieldBuffTime;
    public float attackBuffTimer;
    public  float shieldBuffTimer;

    public bool isHealing;
    public float healingVolume;


    private void Awake()
    {
        inputControl = new PlayerInputControl();
        physicsCheck = GetComponent<PhysicsCheck>();
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerController = GetComponent<PlayerController>();
        character = GetComponent<Character>();
        inputControl.GamePlay.Mikiri.started += Mikiri;
        inputControl.GamePlay.AttackBuff.started += AttackBuff;
        inputControl.GamePlay.ShieldBuff.started += ShieldBuff;
        inputControl.GamePlay.Healing.started += Healing;

        mikiriForceRight = -mikiriForce;

        attackBuffTimer = attackBuffTime;
        shieldBuffTimer = shieldBuffTime;

    }

  
    private void OnEnable()
    {
        inputControl.Enable();
    }
    private void OnDisable()
    {
        inputControl.Disable();
    }


 
    void Update()
    {

       
        if(physicsCheck.isGround &&(!isSkill || isLightSlash) && ! playerController.isAttack && !playerController.isDead && !playerController.isDash)
        {
            LightSlash();
        }

        if (physicsCheck.isGround  && !playerController.isAttack && !playerController.isDead && !playerController.isDash)
        {
            Block();
        }
        if(isBlock && !isShieldBuff)
        {
            character.defenseRate = 0f;
        }
        else if(isBlock == false && !isShieldBuff)
        {
            character.defenseRate = 1f;
        }
        if(attackBuff)
        {
            attackBuffTimer -= Time.deltaTime;
            character.attackRate = 1.5f;
            character.Buff();

            if(attackBuffTimer <= 0f)
            {
                character.attackRate = 1f;
                character.Buff();
                attackBuff = false;
                attackBuffTimer = attackBuffTime;
            }
        }

        if(shieldBuff)
        {
            shieldBuffTimer -= Time.deltaTime;
            if(isBlock)
            {
                character.defenseRate = 0f;
            }
            else character.defenseRate = 0.5f;
            if(shieldBuffTimer <= 0f)
            {
                character.defenseRate = 1f;
                shieldBuff = false;
                shieldBuffTimer = shieldBuffTime;
            }
        }

        

    }
    private void FixedUpdate()
    {
        if (isRoll)
        {
            rb.velocity = new Vector2(playerController.faceDir * playerController.speed * Time.deltaTime * 0.8f, rb.velocity.y);
            playerController.coll.enabled = false;
            if(physicsCheck.touchUpWall)
            {
                rollAgain = true;
            }
        }
        else playerController.coll.enabled = true;
        if (physicsCheck.touchUpWall == false)
        {
           rollAgain = false;
        }
        

        if (isMikiri && !playerController.isDead && !playerController.isDash)
        {
            playerController.isAttack = false;
            if (playerController.faceDir == 1)
            {
                rb.velocity = new Vector2((float)(mikiriForce += 230 * Time.deltaTime), rb.velocity.y);

            }
            else if (playerController.faceDir == -1)
            {
                rb.velocity = new Vector2((float)(mikiriForceRight -= 230 * Time.deltaTime), rb.velocity.y);

            }
            if ((rb.velocity.x >= 0 && playerController.faceDir == 1) || (rb.velocity.x <= 0 && playerController.faceDir == -1))
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    private void Mikiri(InputAction.CallbackContext context)
    {
        if(isMikiri == false && !isSkill && physicsCheck.isGround && !playerController.isDead && !playerController.isHurt && !playerController.isDash)
        {
            character.skillInvulunerable = true;
            int MP = -40;
            if(character.currentMP + MP >= 0)
            {
                mikiriSomke.SetActive(true);
                mikiriDodge.SetActive(true);
                mikiriForce = -40;
                mikiriForceRight = -mikiriForce;
                isSkill = true;
                isMikiri = true;
                rb.velocity = new Vector2((float)mikiriForce * playerController.faceDir, rb.velocity.y);
                character.TriggerInvulnerable();
                character.MPChange(MP);
            }
            
        }
    }

    public void LightSlash()
    {
        if (Input.GetKeyDown(KeyCode.L))//执行一次
        {
            isPressLD = true;
            lightSlashStartTime = Time.time;
        }
        else if(Input.GetKeyUp(KeyCode.L))
        {
            isPressLD = false;
            int MP = -40;
            if(canLightSlash && character.currentMP + MP >= 0)
            {                
                    rb.velocity = new Vector2((float)lightSlashForce * playerController.faceDir, rb.velocity.y);
                    character.invulunerable = true;
                    character.MPChange(MP);               
            }
            else
            {
                canLightSlash = false;
            }
        }

        if(isPressLD)//期间一直执行
        {
            playerController.isAttack = false;
            isSkill = true;
            isLightSlash = true;
            //Debug.Log(Time.time);
            if (Time.time >= lightSlashStartTime + lightSlashTime)
            {
                canLightSlash = true;
            }
        }
    }
    public void LightSlashFinish()
    {
        rb.velocity = Vector2.zero;
    }

    public void Block()
    {
        if (Input.GetKeyDown(KeyCode.K))//执行一次
        {
            if (Mathf.Abs(playerController.inputDirection) >= 0.5f)
            {               
            isRoll = true;
            }
            else isPressBLK = true;
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            isPressBLK = false;
        }

        if(isPressBLK)
        {
            isSkill = true;
        }
        //else
        //{
           // isSkill = false;
            //isBlock = false;
        //}
    }

    public void BlockStart()
    {
        isBlock = true;
    }
    public void BlockFinish()
    {
        isSkill = false;
        isBlock = false;
    }


    private void AttackBuff(InputAction.CallbackContext context)
    {
        if(isSkill == false && physicsCheck.isGround && !playerController.isDead && !playerController.isHurt && !playerController.isDash)
        {
            int MP = -40;
            if(character.currentMP + MP >= 0)
            {
                character.MPChange(MP);
                playerBehavior?.Invoke();
                getBuff = true;
                isSkill = true;
                isAttackBuff = true;
            }
            
        }
    }

    public void AttackBuffFinish()
    {
        getBuff = false;
        isSkill = false;
        isAttackBuff = false;
        attackBuff = true;
        attackBuffTimer = attackBuffTime;
    }
    private void ShieldBuff(InputAction.CallbackContext context)
    {
        if (isSkill == false && physicsCheck.isGround && !playerController.isDead && !playerController.isHurt && !playerController.isDash)
        {
            int MP = -40;
            if (character.currentMP + MP >= 0)
            {
                character.MPChange(MP);
                playerBehavior?.Invoke();
                getBuff = true;
                isSkill = true;
                isShieldBuff = true;
            }
                
        }
    }

    public void ShieldFinish()
    {
        getBuff = false;
        isSkill = false;
        isShieldBuff = false;
        shieldBuff = true;
        shieldBuffTimer = shieldBuffTime;
    }

    private void Healing(InputAction.CallbackContext context)
    {
        if (isSkill == false && physicsCheck.isGround && !playerController.isDead && !playerController.isHurt && !playerController.isDash)
        {
            int MP = -40;
            if (character.currentMP + MP >= 0)
            {
                character.MPChange(MP);
                playerBehavior?.Invoke();
                getBuff = true;
                isSkill = true;
                isHealing = true;
            }
                
        }
    }

    public void HealingFinish()
    {
        getBuff = false;
        isSkill = false;
        isHealing = false;        
        character.HealthChange(healingVolume);
    }

}
