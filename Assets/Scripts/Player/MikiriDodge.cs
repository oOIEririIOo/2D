using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MikiriDodge : MonoBehaviour
{
    public Transform player;
    public PlayerController playerController;
    public PlayerSkill playerSkill;
    public Character character;
    public Vector2 bottomOffsetLeft;
    public Vector2 bottomOffsetRight;

    [Header("时间参数")]
    public float activeTime;//显示时间
    public float activeStart;//开始时间

    public UnityEvent OnSee;
    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerSkill = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkill>();
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        activeStart = Time.time;
        if (playerController.faceDir == 1)
        {
            transform.position = player.position + (Vector3)bottomOffsetLeft;
        }

        if (playerController.faceDir == -1)
        {
            transform.position = player.position + (Vector3)bottomOffsetRight;
        }
    }
    void Update()
    {
        if (Time.time >= activeStart + activeTime)
        {
            gameObject.SetActive(false);
        }
    }

    public void GetAttack(Attack attacker)
    {
        playerSkill.canMikiri = true;
        int MP = 30;
        character.MPChange(MP);
        //StartCoroutine(MikiriPause(100));
    }

    IEnumerator MikiriPause(int duration)
    {
        float pauseTime = duration / 240f;
        Time.timeScale = 0.25f;
        yield return new WaitForSecondsRealtime(pauseTime);
        Time.timeScale = 1;
    }
}
