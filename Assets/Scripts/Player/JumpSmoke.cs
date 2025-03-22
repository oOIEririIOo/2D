using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSmoke : MonoBehaviour
{
    public Transform player;
    public PlayerController playerController;
    public Vector2 bottom0ffset;

    [Header("时间参数")]
    public float activeTime;//显示时间
    public float activeStart;//开始时间


    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        activeStart = Time.time;    
        transform.position = player.position + (Vector3)bottom0ffset;
    }


    void Update()
    {
        if (Time.time >= activeStart + activeTime)
        {
            gameObject.SetActive(false);
        }
    }
}
