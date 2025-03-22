using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraningDummy : MonoBehaviour
{
    Transform attacker;
    private Animator anim;
    private AudioSource hitAudio;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        hitAudio = GetComponent<AudioSource>();
    }
    public void Flipto(Transform attacker)
    {
        if(attacker.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else transform.localScale = new Vector3(-1, 1, 1);
    }

    public void TakeHit(Attack attacker_)
    {
        attacker = attacker_.transform;
        Flipto(attacker);
        anim.SetTrigger("Hit");
        hitAudio.Play();
    }
}
