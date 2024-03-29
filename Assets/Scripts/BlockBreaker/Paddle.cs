﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    public float speed;
    public float rightScreenEdge;
    public float leftScreenEdge;
    public gameManager gm;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.gameOverBool)
        {
            return;  //przestan sie ruszac jak nie ma zyc
        }

        float horizontal = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.right * horizontal * Time.deltaTime * speed);  //by nie zalezala szybkosc od frame
        if(transform.position.x < leftScreenEdge)
        {
            transform.position = new Vector2(leftScreenEdge, transform.position.y);   //blokada by nie wychodzilo poza screen
        }
        if (transform.position.x > rightScreenEdge)
        {
            transform.position = new Vector2(rightScreenEdge, transform.position.y);
        }

    }

    public void moveRight()
    {
        //transform.Translate(Vector2.right * 1 * Time.deltaTime * speed);
        rb.velocity = new Vector2(speed, 0);
    }

    public void moveLeft()
    {
        //transform.Translate(Vector2.right * -1 * Time.deltaTime * speed);
        rb.velocity = new Vector2(-speed, 0);
    }

    public void stay()
    {
        rb.velocity = Vector2.zero;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("extraLife"))
        {
            gm.updateLives(1);  //dodaj jedno zycie przy collision
            Destroy(collision.gameObject);
        }
    }
}
