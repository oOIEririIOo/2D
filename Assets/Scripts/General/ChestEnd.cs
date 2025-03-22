using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestEnd : MonoBehaviour, IInteractable
{
    [Header("¹ã²¥")]
    public VoidEventSO ENDEvent;
    private Animator anim;
    public bool isOpen;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        anim.SetBool("isOpen", isOpen);
    }

    public void TriggerAction()
    {
        StartCoroutine(ENDMenu());
    }

    IEnumerator ENDMenu()
    {
        isOpen = true;
        anim.SetBool("isOpen", isOpen);
        yield return new WaitForSecondsRealtime(3f);
        ENDEvent.OnEventRaised();
    }
}
