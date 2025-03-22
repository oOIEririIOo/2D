using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour , IInteractable
{
    public VoidEventSO saveEvent;

    private Animator anim;
    public GameObject light2D;
    public bool isSave;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        anim.SetBool("isSave", isSave);
        light2D.SetActive(isSave);
        
    }


    public void TriggerAction()
    {
        if(!isSave)
        {
            isSave = true;
            anim.SetBool("isSave", isSave);
            this.gameObject.tag = "Untagged";
            light2D.SetActive(true);
            saveEvent.OnEventRaised();
            GetComponent<AudioSource>().Play();
        }
    }
}
