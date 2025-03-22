using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignText : MonoBehaviour
{
    public GameObject sign;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        sign.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sign.SetActive(false);
    }
}
