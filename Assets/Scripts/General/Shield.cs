using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float damage;


    
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<BossCharacter>()?.TakeShield(this);
    }

   

    

    

}
