using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMove : MonoBehaviour
{
    GameObject mainCam;
    private void Awake()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
    }
    private void FixedUpdate()
    {
        transform.position = new Vector2(mainCam.transform.position.x, transform.position.y);
    }
}
