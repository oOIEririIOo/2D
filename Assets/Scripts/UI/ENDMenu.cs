using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ENDMenu : MonoBehaviour
{
    [Header("¼àÌý")]
    public VoidEventSO ENDEvent;

    [Header("¹ã²¥")]
    public SceneLoadEventSO loadEventSO;


    public GameSceneSO menuScene;
    public Vector3 positionToGo;
    public GameObject firstButton;
    public GameObject endMenu;

    private void OnEnable()
    {
        ENDEvent.OnEventRaised += OpenMenu;
    }

    private void OnDisable()
    {
        ENDEvent.OnEventRaised -= OpenMenu;
    }

    private void OpenMenu()
    {
        endMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstButton);
        Time.timeScale = 0f;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        endMenu.SetActive(false);
        loadEventSO.RaiseLoadRequestEvent(menuScene, positionToGo, true);
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
