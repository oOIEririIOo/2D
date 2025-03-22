using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [Header("�㲥")]
    public ReSpawnEventSO posUpdate;

    [Header("����")]
    public VoidEventSO saveEvent;//����
    public VoidEventSO afterSceneLoadedEvent;//�����³���ʱ��¼��ʼλ��
    public VoidEventSO spawnEvent;//����������

    public Transform playerTrans;
    public Vector3 lastSavePoint;


    private void OnEnable()
    {
        saveEvent.OnEventRaised += OnSaveEvent;
        afterSceneLoadedEvent.OnEventRaised += NewSceneStart;
    }

    private void OnDisable()
    {
        saveEvent.OnEventRaised -= OnSaveEvent;
        afterSceneLoadedEvent.OnEventRaised -= NewSceneStart;
    }

    private void OnSaveEvent()
    {
        lastSavePoint = playerTrans.position;
        posUpdate.RaisedRespawnEvent(lastSavePoint);//����õ�����㲥
    }

    private void NewSceneStart()
    {
        lastSavePoint = playerTrans.position;
        posUpdate.RaisedRespawnEvent(lastSavePoint);//����õ�����㲥
    }
}
