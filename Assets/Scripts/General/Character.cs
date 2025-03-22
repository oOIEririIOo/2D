using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("广播")]
    public VoidEventSO DeadEvent;

    [Header("事件监听")]
    public VoidEventSO NewGameEvent;
    public VoidEventSO NewSceneEvent;
    public VoidEventSO ReSpawnEvent;

    [Header("基本属性")]
    public float maxHealth;
    public float currentHealth;
    public float maxMP;
    public float currentMP;
    public float defenseRate;
    public float attackRate;

    [Header("受伤无敌")]
    public float invulnerableDuration;
    public float invulunerableCounter;
    public bool invulunerable;

    public bool skillInvulunerable;

    public UnityEvent<Character> OnHealthChange;
    public UnityEvent<Character> OnMPChange;
    public UnityEvent<Character> OnBuff;
    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent<Transform> OnDie;


    public AudioSource hitAudio;
    public AudioSource hitFX;
    public AudioSource deadAudio;
    private void Start()
    {
        currentHealth = maxHealth;
        currentMP = maxMP;
        OnHealthChange?.Invoke(this);
        OnMPChange?.Invoke(this);
    }
    public void NewGame()
    {
        if(this.CompareTag("Player"))
        {
            GetComponent<PlayerController>().isDead = false;
            this.gameObject.layer = 7;
        }
        
        currentHealth = maxHealth;
        currentMP = maxMP;
        OnHealthChange?.Invoke(this);
        OnMPChange?.Invoke(this);
    }

    private void OnEnable()
    {
        NewGameEvent.OnEventRaised += NewGame;
        NewSceneEvent.OnEventRaised += NewGame;
        ReSpawnEvent.OnEventRaised += NewGame;

    }

    private void OnDisable()
    {
        NewGameEvent.OnEventRaised -= NewGame;
        NewSceneEvent.OnEventRaised -= NewGame;
        ReSpawnEvent.OnEventRaised -= NewGame;
    }

    private void Update()
    {
        
        if(currentMP < maxMP)
        {
            currentMP += Time.deltaTime * 2f;
            OnMPChange?.Invoke(this);
        }
        else if(currentMP > maxMP)
        {
            currentMP = maxMP;
            OnMPChange?.Invoke(this);
        }
        
        if(invulunerable)
        {
            invulunerableCounter -= Time.deltaTime;
            if(invulunerableCounter <= 0)
            {
                invulunerable = false;
            }
        }
    }

    public void TakeDamage(Attack attacker)
    {
        if (invulunerable || skillInvulunerable)
            return;
        if (currentHealth - (attacker.damage * defenseRate * attacker.attackRate) > 0 && attacker.damage !=0)
        {
            currentHealth -= (attacker.damage * defenseRate * attacker.attackRate);
            TriggerInvulnerable();
            //执行受伤
            OnTakeDamage?.Invoke(attacker.transform);
            if((attacker.damage * defenseRate * attacker.attackRate) !=0)
                HitAudio();
            
        }
        else if (attacker.damage !=0)
        {
            currentHealth = 0;
            gameObject.layer = 2;
            DeadAudio();
            //触发死亡
            OnDie?.Invoke(attacker.transform);
            if(this.CompareTag("Player"))
            {
                DeadEvent.OnEventRaised();
            }
            
        }
        

        OnHealthChange ?. Invoke(this);
        if(attacker.damage >= 30)
        {
            HitPause(36);
        }
        else HitPause(24);
    }

    public void HealthChange(float healthVolume)
    {
       if(currentHealth + healthVolume < maxHealth)
        {
            currentHealth += healthVolume;
            OnHealthChange?.Invoke(this);//广播
        }
        else if(currentHealth + healthVolume >= maxHealth)
        {
            healthVolume = maxHealth - currentHealth;
            currentHealth = maxHealth;
            OnHealthChange?.Invoke(this);//广播
        }
           
        
    }

    public void MPChange(int MP)
    {
        if (currentMP + MP > 0 && MP < 0)
        {
            currentMP += MP;
        }
        else if (MP>0)
        {
            currentMP += MP;
        }
        OnMPChange?.Invoke(this);
    }


    //受伤无敌

    public void TriggerInvulnerable()
    {
        if(!invulunerable)
        {
            invulunerable = true;
            invulunerableCounter = invulnerableDuration;
        }
    }

    //卡肉

    public void HitPause(int duration)
    {
        StartCoroutine(Pause(duration));
    }

    IEnumerator Pause(int duration)
    {
        float pauseTime = duration / 240f;
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(pauseTime);
        Time.timeScale = 1;
    }

    public void Buff()
    {
        OnBuff?.Invoke(this);
    }

    public void HitAudio()
    {
        hitAudio?.Play();
        hitFX?.Play();
    }

    public void DeadAudio()
    {
        deadAudio?.Play();
        hitFX?.Play();
    }
}
