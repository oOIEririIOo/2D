using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine;

public class TimeLineTrigger : MonoBehaviour
{
    public PlayableDirector director;
    protected bool isPlaying;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (isPlaying)
                return;
            director.Play();
            isPlaying = true;
            this.gameObject.SetActive(false);
        }
    }
}
