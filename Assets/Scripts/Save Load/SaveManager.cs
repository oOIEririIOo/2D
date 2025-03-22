using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [Header("广播")]
    public ReSpawnEventSO posUpdate;

    [Header("监听")]
    public VoidEventSO saveEvent;//检查点
    public VoidEventSO afterSceneLoadedEvent;//进入新场景时记录初始位置
    public VoidEventSO spawnEvent;//死亡后重生

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
        posUpdate.RaisedRespawnEvent(lastSavePoint);//将获得的坐标广播
    }

    private void NewSceneStart()
    {
        lastSavePoint = playerTrans.position;
        posUpdate.RaisedRespawnEvent(lastSavePoint);//将获得的坐标广播
    }
}
