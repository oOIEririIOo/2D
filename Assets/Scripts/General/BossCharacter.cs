using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BossCharacter : MonoBehaviour
{
    [Header("事件监听")]
    public VoidEventSO NewGameEvent;
    public VoidEventSO NewSceneEvent;
    public VoidEventSO ReSpawnEvent;


    [Header("基本属性")]
    public float maxHealth;
    public float currentHealth;
    public float defenseRate;
    public float attackRate;

    [Header("受伤无敌")]
    public float invulnerableDuration;
    public float invulunerableCounter;
    public bool invulunerable;



    public UnityEvent<Transform> OnTakeDamage;
    public Image healthBar;
    public Image frame;
    public UnityEvent OnTakeShield;
    public UnityEvent OnDie;
    public AudioSource hitAudio;
    public AudioSource deadAudio;

    public GameObject finish;
    public GameObject bound1;
    public GameObject bound2;
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.fillAmount = 1;

    }

    public void NewGame()
    {
        currentHealth = maxHealth;
        healthBar.fillAmount = 1;
    }

    private void Update()
    {



        if (invulunerable)
        {
            invulunerableCounter -= Time.deltaTime;
            if (invulunerableCounter <= 0)
            {
                invulunerable = false;
            }
        }
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
    public void TakeShield(Shield shielder)
    {
        OnTakeShield? .Invoke();
        HitPause(36);
    }

    public void TakeDamage(Attack attacker)
    {
        if (invulunerable)
            return;
        if (currentHealth - (attacker.damage * defenseRate * attacker.attackRate) > 0 && attacker.damage != 0)
        {
            currentHealth -= (attacker.damage * defenseRate * attacker.attackRate);
            TriggerInvulnerable();
            //执行受伤
            OnTakeDamage?.Invoke(attacker.transform);
            healthBar.fillAmount = currentHealth / maxHealth;
            hitAudio.Play();

        }
        else if (attacker.damage != 0)
        {
            currentHealth = 0;
            //触发死亡
            OnDie?.Invoke();
            healthBar.fillAmount = 0;
            healthBar.enabled = false;
            frame.enabled = false;
            finish.SetActive(false);
            bound2.SetActive(true);
            bound1.SetActive(false);
        }


        if (attacker.damage >= 30)
        {
            HitPause(36);
        }
        else HitPause(24);
    }




    //受伤无敌

    public void TriggerInvulnerable()
    {
        if (!invulunerable)
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

    public void HitAudio()
    {
        hitAudio.Play();
    }

    public void DeadAudio()
    {
        deadAudio.Play();
    }
}


