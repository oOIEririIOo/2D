using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sign : MonoBehaviour
{
    private PlayerInputControl playerInput;
    public GameObject signSprite;
    public Transform playerTrans;
    private IInteractable targetItem;
    private bool canPress;
    private Animator anim;
    private void Awake()
    {
        anim = signSprite.GetComponent<Animator>();
        playerInput = new PlayerInputControl();
        playerInput.Enable();
    }

    private void Update()
    {
        signSprite.SetActive(canPress);
        transform.localScale = playerTrans.localScale;
        
    }

    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.GamePlay.Confirm.started += onConfirm;
    }

    private void OnDisable()
    {
        canPress = false;
        playerInput.Disable();
    }


    private void onConfirm(InputAction.CallbackContext context)
    {
        if(canPress)
        {
            targetItem.TriggerAction();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            canPress = true;
            targetItem = collision.GetComponent<IInteractable>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canPress = false;
    }
}
