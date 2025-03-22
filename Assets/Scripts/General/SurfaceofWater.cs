using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceofWater : MonoBehaviour
{
    GameObject mainCam;
    public float mapWidth;
    float totalWidth;
    public int mapNums;
    public float moveSpeed;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        totalWidth = mapWidth * mapNums;
    }
    private void Update()
    {
        Vector3 _position = transform.position;
        if(mainCam.transform.position.x> transform.position.x + totalWidth / 2f)
        {
            _position.x += totalWidth;
            transform.position = _position;
        }
        else if(mainCam.transform.position.x < transform.position.x - totalWidth / 2f)
        {
            _position.x -= totalWidth;
            transform.position = _position;
        }
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }
}
