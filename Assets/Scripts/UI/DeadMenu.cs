using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeadMenu : MonoBehaviour
{
    [Header("监听")]
    public VoidEventSO DeadEvent; //死亡
    public ReSpawnEventSO spawnPointUpdate;//更新复活地点

    [Header("广播")]
    public SceneLoadEventSO loadEventSO;
    public VoidEventSO reSpawnEvent;

    public GameSceneSO menuScene;
    public Vector3 positionToGo;


    public GameObject deadMenu;
    public GameObject countinueButton;
    public GameObject player;
    public Transform playerTrans;
    public bool isDead;
    public Vector3 reSpawnPos;
    private void OnEnable()
    {
        DeadEvent.OnEventRaised += OnDeadEvent;
        spawnPointUpdate.ReSpawn += OnSpawnPointUpdate;
    }

    private void OnDisable()
    {
        DeadEvent.OnEventRaised += OnDeadEvent;
        spawnPointUpdate.ReSpawn -= OnSpawnPointUpdate;
    }

    private void OnSpawnPointUpdate(Vector3 SpawnPos)
    {
        //Debug.Log(reSpawnPos);
        reSpawnPos = SpawnPos;//更新复活地点
    }

    private void OnDeadEvent()
    {
        StartCoroutine(DeadStart());//打开死亡菜单
    }

    IEnumerator DeadStart()
    {
        yield return new WaitForSecondsRealtime(3f);
        deadMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(countinueButton);
        isDead = true;
    }

    public void ReSpawn()
    {
        playerTrans.position = reSpawnPos;
        reSpawnEvent.OnEventRaised();
        player.GetComponent<PlayerController>().isDead = false;
        player.GetComponent<Character>().NewGame();
        player.SetActive(false);
        isDead = false;
        
        player.SetActive(true);
        deadMenu.SetActive(false);
    }

    public void MainMenu()
    {
        deadMenu.SetActive(false);
        loadEventSO.RaiseLoadRequestEvent(menuScene, positionToGo, true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
