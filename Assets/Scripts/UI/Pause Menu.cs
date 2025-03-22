using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [Header("¹ã²¥")]
    public SceneLoadEventSO loadEventSO;

    public PlayerInputControl inputControl;
    public GameObject pauseMenu;
    public bool inOpen;
    public GameObject countinueButton;

    public GameSceneSO menuScene;
    public Vector3 positionToGo;
    private void Awake()
    {
        inputControl = new PlayerInputControl();
        inputControl.GamePlay.Pause.started += OnPauseMenu;
    }

    

    private void OnEnable()
    {
        inputControl.Enable();
        
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void OnPauseMenu(InputAction.CallbackContext context)
    {
        if(!inOpen)
        {
            inOpen = true;
            pauseMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(countinueButton);
            Time.timeScale = 0f;
        }
        else
        {
            inOpen = false;
            pauseMenu.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
            Time.timeScale = 1f;
        }
    }

    public void Countinue()
    {
        inOpen = false;
        pauseMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        inOpen = false;
        pauseMenu.SetActive(false);
        loadEventSO.RaiseLoadRequestEvent(menuScene, positionToGo, true);
        Time.timeScale = 1f;
    }
    public void ExitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }


}
