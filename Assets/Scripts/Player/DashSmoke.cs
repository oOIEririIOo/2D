using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSmoke : MonoBehaviour
{
    public Transform player;
    public PlayerController playerController;
    public Vector2 bottom0ffsetLeft;
    public Vector2 bottom0ffsetRight;

    [Header("ʱ�����")]
    public float activeTime;//��ʾʱ��
    public float activeStart;//��ʼʱ��


    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        activeStart = Time.time;   
        if(playerController.faceDir == 1)
        {
            transform.position = player.position + (Vector3)bottom0ffsetLeft;
            transform.localScale = new Vector3(1, 1, 1);
        }
            
        if(playerController.faceDir == -1)
        {
            transform.position = player.position + (Vector3)bottom0ffsetRight;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        
    }


    void Update()
    {
        if(Time.time >=activeStart+activeTime)
        {
            gameObject.SetActive(false);
        }
    }

   
}
